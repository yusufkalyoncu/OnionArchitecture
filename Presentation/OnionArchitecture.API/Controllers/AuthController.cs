using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.Application.DTOs.Token;
using OnionArchitecture.Application.Features.Auth.Commands.RefreshToken;
using OnionArchitecture.Application.Features.Auth.Commands.UserLogin;
using OnionArchitecture.Application.Features.Auth.Commands.UserRegister;
using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("login")]
    public async Task<Result<TokenDto>> UserLogin(UserLoginCommand request)
    {
        var res = await _mediator.Send(request);
        return res;
    }
    
    [HttpPost("register")]
    public async Task<Result<TokenDto>> UserRegister(UserRegisterCommand request)
    {
        var res = await _mediator.Send(request);
        return res;
    }

    [HttpPost("refresh-token")]
    public async Task<Result<TokenDto>> RefreshToken(RefreshTokenCommand request)
    {
        var res = await _mediator.Send(request);
        return res;
    }
}