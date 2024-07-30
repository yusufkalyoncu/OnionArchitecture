using FluentValidation;

namespace OnionArchitecture.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.AccessToken)
            .NotNull()
            .NotEmpty()
            .WithName("Access Token");
        
        RuleFor(x => x.RefreshToken)
            .NotNull()
            .NotEmpty()
            .WithName("Refresh Token");
    }
}