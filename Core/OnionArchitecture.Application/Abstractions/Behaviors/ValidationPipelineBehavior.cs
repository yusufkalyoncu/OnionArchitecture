using FluentValidation;
using MediatR;
using OnionArchitecture.Shared;

namespace OnionArchitecture.Application.Abstractions.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        
        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(request, cancellationToken))
        );

        var error = validationResults
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => Error.Validation(failure.ErrorMessage))
            .FirstOrDefault();

        if (error is null)
        {
            return await next();
        }

        return CreateValidationResult<TResponse>(error);
    }

    private static TResult CreateValidationResult<TResult>(Error error)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (Result.Failure(error) as TResult)!;
        }

        var dataType = typeof(TResult).GetGenericArguments()[0];
        var resultType = typeof(Result<>).MakeGenericType(dataType);
        var failureMethod = resultType.GetMethod(nameof(Result.Failure), new[] { typeof(Error) });

        var result = failureMethod!.Invoke(null, new object[] { error });

        return (TResult)result!;
    }
}