package main

import (
	"context"
	"llm-api/internal/db"
	"log"
)

func main() {
	cfg := config{
		addr: ":9090",
	}

	d := db.NewPostgresConnection(context.Background())
	defer d.Close()

	app := application{cfg, d}

	mux := app.mount()

	log.Fatal(app.run(mux))
}
