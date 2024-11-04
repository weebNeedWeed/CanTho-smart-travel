package main

import (
	"database/sql"
	"fmt"
	"net/http"
	"time"
)

type application struct {
	config config
	db     *sql.DB
}

type config struct {
	addr string
}

func (a *application) mount() (mux *http.ServeMux) {
	mux = http.NewServeMux()

	mux.HandleFunc("GET /search", a.searchHandler)
	mux.HandleFunc("POST /gen_itinerary", a.generateItineraryHandler)

	return
}

func (a *application) run(mux *http.ServeMux) error {
	srv := http.Server{
		Addr:         a.config.addr,
		Handler:      mux,
		ReadTimeout:  10 * time.Second,
		WriteTimeout: 30 * time.Second,
		IdleTimeout:  time.Minute,
	}

	fmt.Printf("server is listening on %s\n", a.config.addr)

	return srv.ListenAndServe()
}
