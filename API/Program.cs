var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapCreateDb()
    .MapRobot()
    .MapTestHandleException();

app.Run();
