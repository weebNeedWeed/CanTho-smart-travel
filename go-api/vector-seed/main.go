package main

import (
	"context"
	"database/sql"
	"fmt"
	"log"
	"time"

	"github.com/google/generative-ai-go/genai"
	"github.com/milvus-io/milvus-sdk-go/v2/client"
	"github.com/milvus-io/milvus-sdk-go/v2/entity"
	"google.golang.org/api/option"

	"github.com/lib/pq"
	"github.com/lib/pq/hstore"
)

const (
	milvusAddr          = "https://in03-74c9fd9a603e5e1.serverless.gcp-us-west1.cloud.zilliz.com"
	milvusUserPass      = "db_74c9fd9a603e5e1"
	collectionName      = "destination"
	dim                 = 768
	idCol, embeddingCol = "id", "embeddings"
)

const (
	pgAddr   = "database-1.c2nfqzlurzva.ap-southeast-1.rds.amazonaws.com"
	pgUser   = "postgres"
	pgPass   = "mypassword"
	pgDbName = "SmartTravel"
)

const geminiApiKey = "AIzaSyAIBjBgJQR-zHurK7xHLmU8t3nYLizuLBQ"

func main() {
	ctx := context.Background()
	ctx, ctxCancel := context.WithTimeout(ctx, 15*time.Second)
	defer ctxCancel()

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
		WithField(entity.NewField().WithName(idCol).WithDataType(entity.FieldTypeInt64).WithIsPrimaryKey(true).WithIsAutoID(false)).
		// also the vector field is needed
		WithField(entity.NewField().WithName(embeddingCol).WithDataType(entity.FieldTypeFloatVector).WithDim(dim))

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
		SELECT Destination."Id",Destination."Name","Description","Tags","Amenities","OpeningHours",DestinationCategory."Name" 
		FROM public."Destination" AS Destination
		JOIN public."DestinationCategory" AS DestinationCategory 
		ON Destination."DestinationCategoryId" = DestinationCategory."Id"`)
	if err != nil {
		log.Fatal("failed to query postgres", err)
	}

	genaiClient, err := genai.NewClient(ctx, option.WithAPIKey(geminiApiKey))
	if err != nil {
		log.Fatal("failed to create genai client", err)
	}
	defer genaiClient.Close()
	em := genaiClient.EmbeddingModel("text-embedding-004")

	ids := make([]int64, 0)
	vectors := make([][]float32, 0)

	for rows.Next() {
		var id int64
		var name, description, category string
		var tags, amenities []string
		var openingHours hstore.Hstore
		err := rows.Scan(&id, &name, &description, pq.Array(&tags), pq.Array(&amenities), &openingHours, &category)
		if err != nil {
			log.Fatal("failed to scan row", err)
		}

		ids = append(ids, id)

		// str := name + "\n" + description + "\n" + category + "\n" + strings.Join(tags, ",") + "\n" + strings.Join(amenities, ",") + "\n" + fmt.Sprintf("%v", openingHours)
		str := name + "\n" + description
		emResult, err := em.EmbedContent(ctx, genai.Text(str))
		if err != nil {
			log.Fatal("failed to embed content", err)
		}
		vectors = append(vectors, emResult.Embedding.Values)
	}

	idData := entity.NewColumnInt64(idCol, ids)
	vectorData := entity.NewColumnFloatVector(embeddingCol, dim, vectors)
	_, err = c.Insert(ctx, collectionName, "", idData, vectorData)
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
