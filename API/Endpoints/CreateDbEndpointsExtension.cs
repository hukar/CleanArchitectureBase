namespace API.Endpoints;

public static class CreateDbEndpointsExtension
{
    public static WebApplication MapCreateDb(this WebApplication app)
    {
        var dbBootstrap = app.Services.GetRequiredService<DatabaseBootstrap>();
        
        var route = app.MapGroup("/dbsqlite");

        route.MapGet("/create", async () => await dbBootstrap.CreateDb());

        route.MapGet("/fill", async () => await dbBootstrap.FillDb());

        return app;
    }
}
