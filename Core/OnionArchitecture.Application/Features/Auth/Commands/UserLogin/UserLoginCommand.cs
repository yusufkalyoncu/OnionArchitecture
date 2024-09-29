using MediatR;
using OnionArchitecture.Application.Abstractions.Services;
using OnionArchitecture.Application.DTOs.Token;
using OnionArchitecture.Shared;

namespace OnionArchitecture.Application.Features.Auth.Commands.UserLogin;

public class UserLoginCommand : IRequest<Result<TokenDto>>
{
    public string PhoneOrEmail { get; set; }
    public string Password { get; set; }
}

public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, Result<TokenDto>>
{
    private readonly IUserService _userService;

    public UserLoginCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<TokenDto>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var tokenResult = await _userService.LoginAsync(request.PhoneOrEmail, request.Password);
        if (tokenResult.IsFailure)
        {
            return Result<TokenDto>.Failure(tokenResult.Error!);
        }

        return tokenResult;
    }
}