using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnionArchitecture.API.Extensions;
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
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UserLogin(UserLoginCommand request)
    {
        var res = await _mediator.Send(request);
        return res.ToActionResult();
    }
    
    [HttpPost("register")]
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UserRegister(UserRegisterCommand request)
    {
        var res = await _mediator.Send(request);
        return res.ToActionResult();
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken(RefreshTokenCommand request)
    {
        var res = await _mediator.Send(request);
        return res.ToActionResult();
    }
}