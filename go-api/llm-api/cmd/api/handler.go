package main

import (
	"encoding/json"
	"log"
	"net/http"

	"github.com/google/generative-ai-go/genai"
	_ "github.com/lib/pq"
	"github.com/milvus-io/milvus-sdk-go/v2/client"
	"github.com/milvus-io/milvus-sdk-go/v2/entity"
	"google.golang.org/api/option"
)

const (
	milvusAddr                     = "https://in03-74c9fd9a603e5e1.serverless.gcp-us-west1.cloud.zilliz.com"
	milvusUserPass                 = "db_74c9fd9a603e5e1"
	collectionName                 = "destination"
	dim                            = 768
	idCol, embeddingCol, destIdCol = "id", "embeddings", "destId"
)

const (
	pgAddr   = "database-1.c2nfqzlurzva.ap-southeast-1.rds.amazonaws.com"
	pgUser   = "postgres"
	pgPass   = "mypassword"
	pgDbName = "SmartTravel"
)

const geminiApiKey = "AIzaSyAIBjBgJQR-zHurK7xHLmU8t3nYLizuLBQ"

func (a *application) searchHandler(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Access-Control-Allow-Origin", "*")
	w.Header().Set("Access-Control-Allow-Methods", "*")
	w.Header().Set("Access-Control-Allow-Headers", "*")

	keyword := r.URL.Query().Get("keyword")
	if keyword == "" {
		http.Error(w, "no keyword found", http.StatusBadRequest)
		return
	}

	c, err := client.NewClient(r.Context(), client.Config{
		Address:  milvusAddr,
		Username: milvusUserPass,
		Password: milvusUserPass,
	})
	if err != nil {
		panic(err)
	}

	defer c.Close()

	err = c.LoadCollection(r.Context(), collectionName, false)
	if err != nil {
		panic(err)
	}

	genaiClient, err := genai.NewClient(r.Context(), option.WithAPIKey(geminiApiKey))
	if err != nil {
		log.Fatal("failed to create genai client", err)
	}
	defer genaiClient.Close()
	em := genaiClient.EmbeddingModel("text-embedding-004")

	vectorRes, err := em.EmbedContent(r.Context(), genai.Text(keyword))
	if err != nil {
		panic(err)
	}

	sp, _ := entity.NewIndexAUTOINDEXSearchParam(1)
	vectors := []entity.Vector{entity.FloatVector(vectorRes.Embedding.Values)}

	searchResult, err := c.Search(
		r.Context(),
		collectionName,      // collectionName
		nil,                 // partitionNames
		"",                  // expression
		[]string{destIdCol}, // outputFields
		vectors,             // vectors
		embeddingCol,        // vectorField
		entity.COSINE,       // metricType
		5,                   // topK
		sp,                  // search params
	)
	if err != nil {
		panic(err)
	}

	destIds := make([]int64, 0)
	for _, sr := range searchResult {
		var destIdMilvus *entity.ColumnInt64
		for _, field := range sr.Fields {
			if field.Name() == destIdCol {
				destIdMilvus, _ = field.(*entity.ColumnInt64)
			}
		}

		for i := 0; i < sr.ResultCount; i++ {
			destId, _ := destIdMilvus.ValueByIdx(i)
			destIds = append(destIds, destId)
		}
	}

	// remove duplicate values
	tempMap := make(map[int64]int64)
	for _, id := range destIds {
		tempMap[id] = id
	}

	newIds := make([]int64, 0)
	for id, _ := range tempMap {
		newIds = append(newIds, id)
	}

	// connStr := fmt.Sprintf("host=%s user=%s password=%s dbname=%s sslmode=require", pgAddr, pgUser, pgPass, pgDbName)
	// db, err := sql.Open("postgres", connStr)
	// if err != nil {
	// 	log.Fatal("failed to connect to postgres", err)
	// }
	// defer db.Close()

	// results := make([]map[string]any, 0)

	// for _, id := range newIds {
	// 	rows, _ := db.Query(`
	// 		SELECT "Name", "Description"
	// 		FROM public."Destination"
	// 		WHERE "Id"=$1`, id)
	// 	for rows.Next() {
	// 		var name string
	// 		var description string
	// 		_ = rows.Scan(&name, &description)

	// 		dest := map[string]any{
	// 			"id":          id,
	// 			"name":        name,
	// 			"description": description,
	// 		}
	// 		results = append(results, dest)
	// 	}
	// }

	_ = json.NewEncoder(w).Encode(newIds)
}
