package main

import (
	"fmt"
	"log"
	"net/http"

	"github.com/google/generative-ai-go/genai"
	_ "github.com/lib/pq"
	"github.com/milvus-io/milvus-sdk-go/v2/client"
	"github.com/milvus-io/milvus-sdk-go/v2/entity"
	"google.golang.org/api/option"
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

func (a *application) searchHandler(w http.ResponseWriter, r *http.Request) {
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
		collectionName,  // collectionName
		nil,             // partitionNames
		"",              // expression
		[]string{idCol}, // outputFields
		vectors,         // vectors
		embeddingCol,    // vectorField
		entity.COSINE,   // metricType
		3,               // topK
		sp,              // search params
	)
	if err != nil {
		panic(err)
	}

	for _, sr := range searchResult {
		fmt.Println("ids: ", sr.IDs)
		fmt.Println("Scores: ", sr.Scores)
	}
}
