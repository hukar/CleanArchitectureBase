namespace API.Endpoints;

public static class TestHandleExceptionExtension
{
    public static WebApplication MapTestHandleException(this WebApplication app)
    {
        
        app.MapGet("/exceptiontest", () => {
            throw new TypeLoadException("This is my Type Load Exception 👻");
        });

        return app;
    }
}
