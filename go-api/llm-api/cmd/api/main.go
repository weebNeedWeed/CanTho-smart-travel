package main

import "log"

func main() {
	cfg := config{
		addr: ":9090",
	}

	app := application{cfg}

	mux := app.mount()

	log.Fatal(app.run(mux))
}
