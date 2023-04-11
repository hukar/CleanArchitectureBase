var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.AddApplicationServices();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors(policy => policy
                        .AllowAnyOrigin()
                        //.WithOrigins("http://localhost:5157", "http://localhost:5053/")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        // .WithHeaders("Content-Type")
                        );

app.MapCreateDb()
    .MapRobot()
    .MapTestHandleException();

app.Run();
