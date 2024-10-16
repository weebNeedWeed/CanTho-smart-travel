using API.Areas.Admin.Common;
using Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();
    builder.Services.AddPersistence(builder.Configuration);
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    app.UseStaticFiles();
    app.UseRouting();
    
    app.MapControllerRoute(
        name: AdminAreaName.Value,
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    app.MapControllers();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.Run();
}