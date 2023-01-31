var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRobotRepository, RobotRepositoryInMemory>();
builder.Services.AddAutoMapper(typeof(ApplicationAssembly));
builder.Services.AddMediatR(typeof(ApplicationAssembly));

var app = builder.Build();


app.MapGet("/robots", async (ISender sender) => await sender.Send(new GetAllRobotsQuery()));

app.MapGet("/robots/{id:int}", async (int id,ISender sender) => {
     var robot = await sender.Send(new GetRobotByIdQuery(id));

     if(robot is null) return NotFound();

     return Ok(robot);
});

app.Run();
