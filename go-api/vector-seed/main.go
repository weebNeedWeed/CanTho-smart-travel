package main

import (
	"context"
	"database/sql"
	"fmt"
	"github.com/samber/lo"
	"github.com/sashabaranov/go-openai"
	"log"
	"strings"
	"time"

	"github.com/lib/pq"
	"github.com/lib/pq/hstore"
	"github.com/milvus-io/milvus-sdk-go/v2/client"
	"github.com/milvus-io/milvus-sdk-go/v2/entity"
)

const (
	milvusAddr                     = "https://in03-74c9fd9a603e5e1.serverless.gcp-us-west1.cloud.zilliz.com"
	milvusUserPass                 = "db_74c9fd9a603e5e1"
	collectionName                 = "destination"
	dim                            = 3072
	idCol, embeddingCol, destIdCol = "id", "embeddings", "destId"
)

const (
	pgAddr   = "database-1.c2nfqzlurzva.ap-southeast-1.rds.amazonaws.com"
	pgUser   = "postgres"
	pgPass   = "mypassword"
	pgDbName = "SmartTravel"
)

const geminiApiKey = "AIzaSyAIBjBgJQR-zHurK7xHLmU8t3nYLizuLBQ"
const openaiApiKey = `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`

var genaiClient = openai.NewClientWithConfig(openai.DefaultAzureConfig(openaiApiKey, "https://models.inference.ai.azure.com"))

func main() {
	ctx := context.Background()
	//ctx, ctxCancel := context.WithTimeout(ctx, time.Minute)
	//defer ctxCancel()

	c, err := client.NewClient(ctx, client.Config{
		Address:  milvusAddr,
		Username: milvusUserPass,
		Password: milvusUserPass,
	})
	if err != nil {
		log.Fatal("failed to create client", err)
	}

	colExists, err := c.HasCollection(ctx, collectionName)
	if err != nil {
		log.Fatal("failed to check if collection exists", err)
	}
	if colExists {
		c.DropCollection(ctx, collectionName)
	}

	schema := entity.NewSchema().WithName(collectionName).
		// currently primary key field is compulsory, and only int64 is allowed
		WithField(entity.NewField().WithName(idCol).WithDataType(entity.FieldTypeInt64).WithIsPrimaryKey(true).WithIsAutoID(true)).
		// also the vector field is needed
		WithField(entity.NewField().WithName(embeddingCol).WithDataType(entity.FieldTypeFloatVector).WithDim(dim)).
		WithField(entity.NewField().WithName(destIdCol).WithDataType(entity.FieldTypeInt64))

	err = c.CreateCollection(ctx, schema, entity.DefaultShardNumber)
	if err != nil {
		log.Fatal("failed to create collection:", err.Error())
	}

	connStr := fmt.Sprintf("host=%s user=%s password=%s dbname=%s sslmode=require", pgAddr, pgUser, pgPass, pgDbName)
	db, err := sql.Open("postgres", connStr)
	if err != nil {
		log.Fatal("failed to connect to postgres", err)
	}
	defer db.Close()

	rows, err := db.Query(`
		SELECT Destination."Id",Destination."Name","Description","Tags","Amenities","OpeningHours",DestinationCategory."Name","Pricing"
		FROM public."Destination" AS Destination
		JOIN public."DestinationCategory" AS DestinationCategory 
		ON Destination."DestinationCategoryId" = DestinationCategory."Id"`)
	if err != nil {
		log.Fatal("failed to query postgres", err)
	}

	//genaiClient, err := genai.NewClient(ctx, option.WithAPIKey(geminiApiKey))
	//if err != nil {
	//	log.Fatal("failed to create genai client", err)
	//}
	//defer genaiClient.Close()
	//em := genaiClient.EmbeddingModel("text-embedding-004")

	destIds := make([]int64, 0)
	vectors := make([][]float32, 0)

	for rows.Next() {
		var id int64
		var name, description, category string
		var tags, amenities []string
		var openingHours, pricing hstore.Hstore
		err := rows.Scan(&id, &name, &description, pq.Array(&tags), pq.Array(&amenities), &openingHours, &category, &pricing)
		if err != nil {
			log.Fatal("failed to scan row", err)
		}
		e := make([]string, 0)
		descChunks := makeChunks(description)
		for _, chunk := range descChunks {
			e = append(e, chunk)
		}

		//str := name + "\n" + category + "\n" + strings.Join(tags, ",") + "\n" + strings.Join(amenities, ",") + "\n" + fmt.Sprintf("%v", openingHours)
		str := fmt.Sprintf("%s %s %v", name, category, fmt.Sprintf("%v", openingHours))
		e = append(e, str)

		//str := name + "\n" + category + "\n" + strings.Join(tags, ",") + "\n" + strings.Join(amenities, ",") + "\n" + fmt.Sprintf("%v", openingHours)
		str = fmt.Sprintf(`%s %s %s`, strings.Join(tags, ","), strings.Join(amenities, ","), fmt.Sprintf("%v", pricing))
		e = append(e, str)

		t := embed(ctx, e)
		for _, v := range t {
			destIds = append(destIds, id)
			vectors = append(vectors, v)
		}

		time.Sleep(10 * time.Second)
	}

	destIdsData := entity.NewColumnInt64(destIdCol, destIds)
	vectorData := entity.NewColumnFloatVector(embeddingCol, dim, vectors)
	_, err = c.Insert(ctx, collectionName, "", destIdsData, vectorData)
	if err != nil {
		log.Fatal("fail to insert data", err)
	}

	index, err := entity.NewIndexAUTOINDEX(entity.MetricType("COSINE"))
	if err != nil {
		log.Fatal("fail to create index", err)
	}
	err = c.CreateIndex(ctx, collectionName, embeddingCol, index, false)
	if err != nil {
		log.Fatal("fail to create index", err)
	}
}

func makeChunks(desc string) (res []string) {
	res = make([]string, 0)
	size := 200
	ableToChunk := len(desc) / size

	for i := 1; i <= ableToChunk; i++ {
		str := desc[(size * (i - 1)):(size * i)]
		res = append(res, strings.ToValidUTF8(str, ""))
	}

	res = append(res, strings.ToValidUTF8(desc[(size*(ableToChunk)):], ""))

	return
}

func embed(ctx context.Context, input []string) [][]float32 {
	resp, err := genaiClient.CreateEmbeddings(
		ctx,
		openai.EmbeddingRequest{
			Input: input,
			Model: openai.LargeEmbedding3,
		})
	if err != nil {
		panic(err)
	}

	return lo.Map(resp.Data, func(item openai.Embedding, index int) []float32 {
		return item.Embedding
	})
}
