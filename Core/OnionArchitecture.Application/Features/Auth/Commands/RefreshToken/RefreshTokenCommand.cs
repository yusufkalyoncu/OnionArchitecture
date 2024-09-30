using MediatR;
using OnionArchitecture.Application.Abstractions.Messaging;
using OnionArchitecture.Application.Abstractions.Services;
using OnionArchitecture.Application.DTOs.Token;
using OnionArchitecture.Shared;

namespace OnionArchitecture.Application.Features.Auth.Commands.RefreshToken;

public sealed record RefreshTokenCommand(
    string AccessToken,
    string RefreshToken) : ICommand<TokenDto>;

public sealed class RefreshTokenCommandHandler(IUserService userService) 
    : ICommandHandler<RefreshTokenCommand, TokenDto>
{
    public async Task<Result<TokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var tokenResult = await userService.RefreshToken(request.AccessToken, request.RefreshToken);
        if (tokenResult.IsFailure)
        {
            return Result<TokenDto>.Failure(tokenResult.Error!);
        }

        return tokenResult;
    }
}