package db

import (
	"context"
	"database/sql"
	"fmt"
	"time"

	_ "github.com/lib/pq"
	"github.com/milvus-io/milvus-sdk-go/v2/client"
)

const (
	milvusAddr     = "https://in03-74c9fd9a603e5e1.serverless.gcp-us-west1.cloud.zilliz.com"
	milvusUserPass = "db_74c9fd9a603e5e1"
)

const (
	pgAddr   = "database-1.c2nfqzlurzva.ap-southeast-1.rds.amazonaws.com"
	pgUser   = "postgres"
	pgPass   = "mypassword"
	pgDbName = "SmartTravel"
)

func NewMilvusClient(ctx context.Context) client.Client {
	c, err := client.NewClient(ctx, client.Config{
		Address:  milvusAddr,
		Username: milvusUserPass,
		Password: milvusUserPass,
	})
	if err != nil {
		panic(err)
	}
	return c
}

func NewPostgresConnection(ctx context.Context) *sql.DB {
	connStr := fmt.Sprintf("host=%s user=%s password=%s dbname=%s sslmode=require", pgAddr, pgUser, pgPass, pgDbName)
	conn, err := sql.Open("postgres", connStr)
	if err != nil {
		panic(err)
	}

	newCtx, cancel := context.WithTimeout(ctx, 5*time.Second)
	defer cancel()

	err = conn.PingContext(newCtx)
	if err != nil {
		panic(err)
	}

	return conn
}
