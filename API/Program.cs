var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

var dbBootstrap = app.Services.GetRequiredService<DatabaseBootstrap>();
await dbBootstrap.Bootstrap();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapRobot();
app.MapTestHandleException();

app.Run();
