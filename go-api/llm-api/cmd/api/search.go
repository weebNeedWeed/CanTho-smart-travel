package main

import (
	"encoding/json"
	"fmt"
	"github.com/sashabaranov/go-openai"
	"llm-api/internal/ai"
	"llm-api/internal/db"
	"llm-api/internal/helper"
	"net/http"

	_ "github.com/lib/pq"
	"github.com/milvus-io/milvus-sdk-go/v2/entity"
)

const (
	collectionName                 = "destination"
	dim                            = 3072
	idCol, embeddingCol, destIdCol = "id", "embeddings", "destId"
)

func (a *application) searchHandler(w http.ResponseWriter, r *http.Request) {
	helper.AllowCORS(w.Header())

	keyword := r.URL.Query().Get("keyword")
	if keyword == "" {
		http.Error(w, "no keyword found", http.StatusBadRequest)
		return
	}

	c := db.NewMilvusClient(r.Context())
	defer c.Close()

	err := c.LoadCollection(r.Context(), collectionName, false)
	if err != nil {
		panic(err)
	}

	//genaiClient := ai.NewGenaiClient(r.Context())
	//defer genaiClient.Close()
	//em := genaiClient.EmbeddingModel("text-embedding-004")
	openaiClient := ai.NewOpenAIClient()

	//vectorRes, err := em.EmbedContent(r.Context(), genai.Text(keyword))
	vectorRes, err := openaiClient.CreateEmbeddings(
		r.Context(),
		openai.EmbeddingRequest{
			Input: []string{keyword},
			Model: openai.LargeEmbedding3,
		})
	if err != nil {
		panic(err)
	}

	sp, _ := entity.NewIndexAUTOINDEXSearchParam(2)
	sp.AddRadius(0.4)
	vectors := []entity.Vector{entity.FloatVector(vectorRes.Data[0].Embedding)}

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
		fmt.Println("data:", sr.Fields.GetColumn(destIdCol))
		fmt.Println("scores:", sr.Scores)
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
