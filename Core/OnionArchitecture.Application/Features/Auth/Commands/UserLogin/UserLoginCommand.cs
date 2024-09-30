using OnionArchitecture.Application.Abstractions.Messaging;
using OnionArchitecture.Application.Abstractions.Services;
using OnionArchitecture.Application.DTOs.Token;
using OnionArchitecture.Shared;

namespace OnionArchitecture.Application.Features.Auth.Commands.UserLogin;

public sealed record UserLoginCommand(
    string PhoneOrEmail,
    string Password) : ICommand<TokenDto>;

public sealed class UserLoginCommandHandler(IUserService userService) 
    : ICommandHandler<UserLoginCommand, TokenDto>
{
    public async Task<Result<TokenDto>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var tokenResult = await userService.LoginAsync(request.PhoneOrEmail, request.Password);
        if (tokenResult.IsFailure)
        {
            return Result<TokenDto>.Failure(tokenResult.Error!);
        }

        return tokenResult;
    }
}