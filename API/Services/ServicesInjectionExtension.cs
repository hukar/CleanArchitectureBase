namespace API.Services;

public static class ServicesInjectionExtension
{
    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        // builder.Services.AddSingleton<IRobotRepository, RobotRepositoryInMemory>();
        builder.Services.AddSingleton<IRobotRepository, RobotRepositoryInDb>();
        builder.Services.AddSingleton<DapperContext>();
        builder.Services.AddSingleton<DatabaseBootstrap>();
        builder.Services.AddAutoMapper(typeof(ApplicationAssembly));
        builder.Services.AddMediatR(typeof(ApplicationAssembly));
        builder.Services.AddValidatorsFromAssemblyContaining<ApplicationAssembly>();
    }
}
