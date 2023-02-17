namespace API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
        private readonly ILogger _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.GetType().ToString());
            _logger.LogError( $"💀 : {ex.Message}");
            await HandleExceptionAsync(context.Response);
        }
    }

    private async Task HandleExceptionAsync(HttpResponse response)
    {
        response.ContentType = "application/json";
        response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await response.WriteAsJsonAsync(new {
            StatusCode = response.StatusCode,
            Message = "Something wrond was happened !"
        });
    }
}
