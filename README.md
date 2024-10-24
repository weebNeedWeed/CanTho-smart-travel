```md
dotnet ef migrations add Init --startup-project ./dotnet-api/API --project ./dotnet-api/Persistence

dotnet ef migrations remove --startup-project ./dotnet-api/API --project ./dotnet-api/Persistence

dotnet ef database update --startup-project ./dotnet-api/API --project ./dotnet-api/Persistence
```