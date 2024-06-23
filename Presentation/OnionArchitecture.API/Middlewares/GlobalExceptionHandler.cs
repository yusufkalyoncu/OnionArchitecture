using System.Net;
using System.Text.Json;
using OnionArchitecture.Domain.Shared;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly string _unknownErrorMessage = "An unexpected error occurred";

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Exception Occurred: {Message}", exception.Message);

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        Error error;

        if (exception is BaseException customException)
        {
            error = GetError(customException);
        }
        else
        {
            error = Error.Server(_unknownErrorMessage);
        }

        var result = Result<object>.Failure(error);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 200;
        await context.Response.WriteAsync(JsonSerializer.Serialize(result, options));
    }

    private Error GetError(BaseException exception)
    {
        return exception.Status switch
        {
            HttpStatusCode.BadRequest => Error.BadRequest(exception.Error),
            HttpStatusCode.Unauthorized => Error.Unauthorized(exception.Error),
            HttpStatusCode.NotFound => Error.NotFound(exception.Error),
            HttpStatusCode.Conflict => Error.Conflict(exception.Error),
            HttpStatusCode.InternalServerError => Error.Server(exception.Error),
            _ => Error.Server(_unknownErrorMessage)
        };
    }
}
