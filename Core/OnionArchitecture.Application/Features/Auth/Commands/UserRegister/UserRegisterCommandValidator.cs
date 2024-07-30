using FluentValidation;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Domain.ValueObjects;

namespace OnionArchitecture.Application.Features.Auth.Commands.UserRegister;

public class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
{
    public UserRegisterCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(Name.FirstNameMinLength)
            .MaximumLength(Name.FirstNameMaxLength)
            .WithName("First name");
        
        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(Name.LastNameMinLength)
            .MaximumLength(Name.LastNameMaxLength)
            .WithName("Last name");

        RuleFor(x => x.Phone)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.CountryCode)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(User.PasswordMinLength)
            .MaximumLength(User.PasswordMaxLength);
        
        RuleFor(x => x.PasswordConfirm)
            .NotNull()
            .NotEmpty()
            .MinimumLength(User.PasswordMinLength)
            .MaximumLength(User.PasswordMaxLength);
    }
}