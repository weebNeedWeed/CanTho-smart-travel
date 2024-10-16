using Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    
    builder.Services.AddPersistence(builder.Configuration);
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    app.MapControllers();
    app.Run();
}