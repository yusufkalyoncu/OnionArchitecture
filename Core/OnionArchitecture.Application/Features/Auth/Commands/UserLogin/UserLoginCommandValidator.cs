using FluentValidation;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Features.Auth.Commands.UserLogin;

public class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
{
    public UserLoginCommandValidator()
    {
        RuleFor(x => x.PhoneOrEmail)
            .NotNull()
            .NotEmpty()
            .WithName("Username");

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(User.PasswordMinLength)
            .MaximumLength(User.PasswordMaxLength);
    }
}