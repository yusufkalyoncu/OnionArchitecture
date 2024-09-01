using FluentValidation;
using MediatR;
using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.Application.Abstractions.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>>
{
    private readonly IValidator<TRequest> _validator;

    public ValidationPipelineBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<Result<TResponse>> Handle(
        TRequest request,
        RequestHandlerDelegate<Result<TResponse>> next,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<TResponse>.Failure(Error.Validation(validationResult.Errors.First().ErrorMessage));
        }

        return await next();
    }
}