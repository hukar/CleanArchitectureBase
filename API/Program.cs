using Application.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRobotRepository, RobotRepositoryInMemory>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/robots", async (IRobotRepository repo) => await repo.GetAll());

app.Run();
