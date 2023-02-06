var builder = WebApplication.CreateBuilder(args);

// System.Console.WriteLine(args[0]);
// System.Console.WriteLine(args[1]);
// System.Console.WriteLine(args[2]);
// System.Console.WriteLine(args[3]);

builder.Services.AddSingleton<IRobotRepository, RobotRepositoryInMemory>();
builder.Services.AddAutoMapper(typeof(ApplicationAssembly));
builder.Services.AddMediatR(typeof(ApplicationAssembly));
builder.Services.AddValidatorsFromAssemblyContaining<ApplicationAssembly>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapRobot();
app.MapTestHandleException();

app.Run();
