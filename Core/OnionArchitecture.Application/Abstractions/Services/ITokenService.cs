using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.Application.Abstractions.Services;

public interface ITokenService
{
    public Result<string> CreateAccessToken(User user);
    public Result<string> GenerateRefreshToken();
    public Result<string> GetUserIdFromJwtToken(string jwtToken);
}