using MediatR;
using OnionArchitecture.Application.Abstractions.Services;
using OnionArchitecture.Application.DTOs.Token;
using OnionArchitecture.Shared;

namespace OnionArchitecture.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<Result<TokenDto>>
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<TokenDto>>
{
    private readonly IUserService _userService;

    public RefreshTokenCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<TokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var tokenResult = await _userService.RefreshToken(request.AccessToken, request.RefreshToken);
        if (tokenResult.IsFailure)
        {
            return Result<TokenDto>.Failure(tokenResult.Error!);
        }

        return tokenResult;
    }
}