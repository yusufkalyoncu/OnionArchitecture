using OnionArchitecture.Application.Abstractions.Messaging;
using OnionArchitecture.Application.Abstractions.Services;
using OnionArchitecture.Application.DTOs.Auth.Requests;
using OnionArchitecture.Application.DTOs.Token;
using OnionArchitecture.Shared;

namespace OnionArchitecture.Application.Features.Auth.Commands.UserRegister;

public sealed record UserRegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string CountryCode,
    string Phone,
    string Password,
    string PasswordConfirm) : ICommand<TokenDto>;

public sealed class UserRegisterCommandHandler(IUserService userService) 
    : ICommandHandler<UserRegisterCommand, TokenDto>
{
    public async Task<Result<TokenDto>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var userRegisterRequest = new UserRegisterRequest(
            request.FirstName,
            request.LastName,
            request.Email,
            request.CountryCode,
            request.Phone,
            request.Password,
            request.PasswordConfirm);
        
        var tokenResult = await userService.RegisterAsync(userRegisterRequest);
        if (tokenResult.IsFailure)
        {
            return Result<TokenDto>.Failure(tokenResult.Error!);
        }

        return tokenResult;
    }
}