package main

import (
	"bytes"
	"encoding/json"
	"fmt"
	"github.com/lib/pq"
	"github.com/lib/pq/hstore"
	"github.com/milvus-io/milvus-sdk-go/v2/entity"
	"github.com/sashabaranov/go-openai"
	"github.com/sashabaranov/go-openai/jsonschema"
	"llm-api/internal/ai"
	"llm-api/internal/db"
	"llm-api/internal/helper"
	"net/http"
	"path/filepath"
	"slices"
	"strings"
	"text/template"
	"time"
)

type Destination struct {
	Id                      int
	Name                    string
	ShortDescription        string
	Tags                    []string
	Amenities               []string
	OpeningHours            hstore.Hstore
	Pricing                 hstore.Hstore
	Distance                float64
	Long                    float64
	Lat                     float64
	DestinationCategoryName string
}

type PostPayload struct {
	UserId      int     `json:"userId"`
	ItineraryId int     `json:"itineraryId"`
	UserLat     float64 `json:"userLat"`
	UserLng     float64 `json:"userLng"`
}

func (a *application) generateItineraryHandler(w http.ResponseWriter, r *http.Request) {
	helper.AllowCORS(w.Header())
	if r.Method == http.MethodOptions {
		return
	}

	w.Header().Add("Content-Type", "application/json")

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

	var startDStr, endDStr string
	q = `
		SELECT "StartDate","EndDate" FROM public."Itinerary"
		WHERE "Id"=$1
	`
	err = a.db.QueryRowContext(r.Context(), q, payload.ItineraryId).
		Scan(&startDStr, &endDStr)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}
	startD, _ := time.Parse(time.RFC3339, startDStr)
	endD, _ := time.Parse(time.RFC3339, endDStr)

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

	sp, _ := entity.NewIndexAUTOINDEXSearchParam(2)
	sp.AddRadius(0.3)
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
		15,                  // topK
		sp,                  // search params
	)
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}

	destIds := make([]int64, 0)
	for _, sr := range searchResult {
		//fmt.Println("data:", sr.Fields.GetColumn(destIdCol))
		//fmt.Println("scores:", sr.Scores)
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

	q = `
		SELECT d."Id",d."Name","ShortDescription","Tags","Amenities","OpeningHours","Pricing",ROUND(ST_DistanceSphere(ST_GeometryFromText($1,4326),"Location")), dc."Name", ST_X("Location"), ST_Y("Location") FROM public."Destination" as d
		JOIN public."DestinationCategory" AS dc ON d."DestinationCategoryId"=dc."Id"                                                                                                                       
		WHERE d."Id" = ANY($2::int[])
	`
	rows, err := a.db.QueryContext(r.Context(), q, fmt.Sprintf("POINT(%v %v)", payload.UserLat, payload.UserLng), pq.Array(newDestIds))
	if err != nil {
		http.Error(w, err.Error(), http.StatusBadRequest)
		return
	}
	dests := make([]Destination, 0)
	for rows.Next() {
		var d Destination
		err = rows.Scan(&d.Id, &d.Name, &d.ShortDescription, pq.Array(&d.Tags),
			pq.Array(&d.Amenities), &d.OpeningHours, &d.Pricing, &d.Distance, &d.DestinationCategoryName, &d.Lat, &d.Long)
		if err != nil {
			panic(err)
		}
		dests = append(dests, d)
	}

	type ChatCompletionResult struct {
		EndDate        string  `json:"endDate"`
		TotalCost      float64 `json:"totalCost"`
		ItineraryItems []struct {
			DestinationId   int    `json:"destinationId"`
			DestinationName string `json:"destinationName"`
			StartTime       string `json:"startTime"`
			EndTime         string `json:"endTime"`
			Priority        int    `json:"priority"`
			Note            string `json:"note"`
		} `json:"itineraryItems"`
	}
	result := new(ChatCompletionResult)
	schema, err := jsonschema.GenerateSchemaForType(result)
	if err != nil {
		panic(err)
	}

	tmplFolder, _ := filepath.Abs("./templates")
	var instruction = &bytes.Buffer{}
	var prompt = &bytes.Buffer{}

	instructionTempl := template.Must(template.ParseFiles(tmplFolder + "/instruction.tmpl"))
	err = instructionTempl.Execute(instruction, nil)
	if err != nil {
		panic(err)
	}

	type PromptParams struct {
		Dests           []Destination
		BMin, BMax      float64
		PTags           string
		EndDate         string
		StartDate       string
		CurrentLocation struct {
			Lat float64
			Lng float64
		}
	}

	p := PromptParams{
		Dests:     dests,
		BMin:      bMin,
		BMax:      bMax,
		PTags:     strings.Join(pTags, ","),
		EndDate:   endD.Format(time.DateOnly),
		StartDate: startD.Format(time.DateOnly),
		CurrentLocation: struct {
			Lat float64
			Lng float64
		}{payload.UserLat, payload.UserLng},
	}

	prompTempl := template.Must(template.ParseFiles(tmplFolder + "/default.tmpl"))
	err = prompTempl.Execute(prompt, p)
	if err != nil {
		panic(err)
	}

	resp, err := openaiClient.CreateChatCompletion(r.Context(), openai.ChatCompletionRequest{
		Model: openai.GPT4o,
		Messages: []openai.ChatCompletionMessage{
			{
				Role:    openai.ChatMessageRoleSystem,
				Content: instruction.String(),
			},
			{
				Role:    openai.ChatMessageRoleUser,
				Content: prompt.String(),
			},
		},
		Temperature: 0.2,
		N:           1,
		ResponseFormat: &openai.ChatCompletionResponseFormat{
			Type: openai.ChatCompletionResponseFormatTypeJSONSchema,
			JSONSchema: &openai.ChatCompletionResponseFormatJSONSchema{
				Name:   "itinerary_information",
				Schema: schema,
				Strict: true,
			},
		},
	})
	if err != nil {
		panic(err)
	}
	var respAsObj ChatCompletionResult
	err = json.Unmarshal([]byte(resp.Choices[0].Message.Content), &respAsObj)
	if err != nil {
		panic(err)
	}

	type ItineraryItem struct {
		DestinationId   int     `json:"destinationId"`
		DestinationName string  `json:"destinationName"`
		StartTime       string  `json:"startTime"`
		EndTime         string  `json:"endTime"`
		Priority        int     `json:"priority"`
		Note            string  `json:"note"`
		Lat             float64 `json:"lat"`
		Lng             float64 `json:"lng"`
	}
	type ResponseData struct {
		EndDate        string          `json:"endDate"`
		TotalCost      float64         `json:"totalCost"`
		ItineraryItems []ItineraryItem `json:"itineraryItems"`
	}

	itemList := make([]ItineraryItem, 0)
	for _, i := range respAsObj.ItineraryItems {
		item := ItineraryItem{
			DestinationId: i.DestinationId,
			StartTime:     i.StartTime,
			EndTime:       i.EndTime,
			Priority:      i.Priority,
			Note:          i.Note,
		}
		idx := slices.IndexFunc(dests, func(d Destination) bool {
			return d.Id == i.DestinationId
		})
		if idx != -1 {
			item.Lat = dests[idx].Lat
			item.Lng = dests[idx].Long
			item.DestinationName = dests[idx].Name
		}
		itemList = append(itemList, item)
	}
	responseData := ResponseData{
		EndDate:        respAsObj.EndDate,
		TotalCost:      respAsObj.TotalCost,
		ItineraryItems: itemList,
	}
	err = json.NewEncoder(w).Encode(responseData)
	if err != nil {
		panic(err)
	}
}
