using MediatR;
using OnionArchitecture.Application.Abstractions.Services;
using OnionArchitecture.Application.DTOs.Auth.Requests;
using OnionArchitecture.Application.DTOs.Token;
using OnionArchitecture.Shared;

namespace OnionArchitecture.Application.Features.Auth.Commands.UserRegister;

public class UserRegisterCommand : IRequest<Result<TokenDto>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string CountryCode { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
}

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, Result<TokenDto>>
{
    private readonly IUserService _userService;

    public UserRegisterCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

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
        
        var tokenResult = await _userService.RegisterAsync(userRegisterRequest);
        if (tokenResult.IsFailure)
        {
            return Result<TokenDto>.Failure(tokenResult.Error!);
        }

        return tokenResult;
    }
}