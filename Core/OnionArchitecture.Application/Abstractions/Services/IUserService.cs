using OnionArchitecture.Application.DTOs.Auth.Requests;
using OnionArchitecture.Application.DTOs.Token;
using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.Application.Abstractions.Services;

public interface IUserService
{
    Task<Result<TokenDto>> RegisterAsync(UserRegisterRequest request);
    public Task<Result<TokenDto>> LoginAsync(string phoneOrEmail, string password);
    public Task<Result<TokenDto>> RefreshToken(string accessToken, string refreshToken);
}