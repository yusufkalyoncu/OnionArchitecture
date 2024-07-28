using MediatR;
using Microsoft.Extensions.Logging;
using OnionArchitecture.Domain.Shared;
using Serilog.Context;

namespace OnionArchitecture.Application.Abstractions.Behaviors;

internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest,TResponse>
    where TRequest : class
    where TResponse : Result
{
    private readonly ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public RequestLoggingPipelineBehavior(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        
        _logger.LogInformation($"Processing request {requestName}");

        TResponse result = await next();

        if (result.IsSuccess)
        {
            _logger.LogInformation($"Completed request {requestName} with successfully");
        }
        else
        {
            using (LogContext.PushProperty("Error", result.Error, true))
            {
                _logger.LogError($"Completed request {requestName} with error");
            }
        }

        return result;
    }
}