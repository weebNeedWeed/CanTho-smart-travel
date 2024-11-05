package ai

import (
	"context"
	"github.com/sashabaranov/go-openai"
	"log"

	"github.com/google/generative-ai-go/genai"
	geminiOption "google.golang.org/api/option"
)

const geminiApiKey = "AIzaSyAIBjBgJQR-zHurK7xHLmU8t3nYLizuLBQ"

const openaiApiKey = `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`

func NewGenaiClient(ctx context.Context) *genai.Client {
	genaiClient, err := genai.NewClient(ctx, geminiOption.WithAPIKey(geminiApiKey))
	if err != nil {
		log.Fatal("failed to create genai client", err)
	}
	return genaiClient
}

func NewOpenAIClient() *openai.Client {
	cfg := openai.DefaultAzureConfig(openaiApiKey, "https://models.inference.ai.azure.com")
	cfg.APIVersion = "2024-08-01-preview"
	client := openai.NewClientWithConfig(cfg)
	return client
}
