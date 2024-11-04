package main

import (
	"encoding/json"
	"fmt"
	"github.com/lib/pq"
	"github.com/milvus-io/milvus-sdk-go/v2/entity"
	"github.com/sashabaranov/go-openai"
	"llm-api/internal/ai"
	"llm-api/internal/db"
	"llm-api/internal/helper"
	"net/http"
	"strings"
)

type PostPayload struct {
	UserId      int `json:"userId"`
	ItineraryId int `json:"itineraryId"`
}

func (a *application) generateItineraryHandler(w http.ResponseWriter, r *http.Request) {
	helper.AllowCORS(w.Header())
	w.Header().Set("Content-Type", "application/json")

	var payload PostPayload
	_ = json.NewDecoder(r.Body).Decode(&payload)

	q := `
		SELECT "BudgetMin","BudgetMax","PreferenceTags" FROM public."TravelPreference"
		WHERE "UserId"=$1
	`

	var bMin, bMax float64
	var pTags []string
	err := a.db.QueryRowContext(r.Context(), q, payload.UserId).
		Scan(&bMin, &bMax, pq.Array(&pTags))
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	var startD, endD string
	q = `
		SELECT "StartDate","EndDate" FROM public."Itinerary"
		WHERE "Id"=$1
	`
	err = a.db.QueryRowContext(r.Context(), q, payload.ItineraryId).
		Scan(&startD, &endD)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	openaiClient := ai.NewOpenAIClient()
	tagsAsEmbed, err := openaiClient.CreateEmbeddings(
		r.Context(),
		openai.EmbeddingRequest{
			Input: []string{strings.Join(pTags, ",")},
			Model: openai.LargeEmbedding3,
		})
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	c := db.NewMilvusClient(r.Context())
	defer c.Close()

	err = c.LoadCollection(r.Context(), collectionName, false)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	sp, _ := entity.NewIndexAUTOINDEXSearchParam(1)
	vectors := []entity.Vector{entity.FloatVector(tagsAsEmbed.Data[0].Embedding)}

	searchResult, err := c.Search(
		r.Context(),
		collectionName,      // collectionName
		nil,                 // partitionNames
		"",                  // expression
		[]string{destIdCol}, // outputFields
		vectors,             // vectors
		embeddingCol,        // vectorField
		entity.COSINE,       // metricType
		10,                  // topK
		sp,                  // search params
	)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
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

	tmp := make(map[int64]int8)
	for _, id := range destIds {
		tmp[id] = 0
	}

	newDestIds := make([]int64, 0)
	for k, _ := range tmp {
		newDestIds = append(newDestIds, k)
	}

	w.WriteHeader(http.StatusOK)
}
