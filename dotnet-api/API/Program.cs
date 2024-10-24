using API;
using API.Areas.Admin.Common;
using Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPersistence(builder.Configuration)
        .AddAPI(builder.Configuration);
}

var app = builder.Build();
{
    app.UseStaticFiles();
    app.UseRouting();

    app.UseCors("cors");
    
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.MapControllerRoute(
        name: AdminAreaName.Value,
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    app.UseStaticFiles();
    app.MapControllers();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.Run();
}