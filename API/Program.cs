var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRobotRepository, RobotRepositoryInMemory>();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddAutoMapper(typeof(ApplicationAssembly));
builder.Services.AddMediatR(typeof(ApplicationAssembly));
builder.Services.AddValidatorsFromAssemblyContaining<ApplicationAssembly>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapRobot();
app.MapTestHandleException();

app.Run();
