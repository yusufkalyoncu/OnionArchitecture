using System.Net;
using System.Text.Json;
using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.API.Middlewares;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private const string UnknownErrorMessage = "An unexpected error occurred";

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
            error = Error.Server(UnknownErrorMessage);
        }

        var result = Result.Failure(error);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = error.StatusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(result.Error, options));
    }

    private Error GetError(BaseException exception)
    {
        return exception.Status switch
        {
            HttpStatusCode.BadRequest => Error.BadRequest(exception.Message),
            HttpStatusCode.Unauthorized => Error.Unauthorized(exception.Message),
            HttpStatusCode.NotFound => Error.NotFound(exception.Message),
            HttpStatusCode.Conflict => Error.Conflict(exception.Message),
            HttpStatusCode.InternalServerError => Error.Server(exception.Message),
            _ => Error.Server(UnknownErrorMessage)
        };
    }
}