var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.AddApplicationServices();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors(policy => policy
                        .WithOrigins("http://localhost:5157")
                        .AllowAnyMethod()
                        .WithHeaders("Content-Type"));

app.MapCreateDb()
    .MapRobot()
    .MapTestHandleException();

app.Run();
