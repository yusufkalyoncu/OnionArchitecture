using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnionArchitecture.Application.Abstractions.Services;
using OnionArchitecture.Application.Options;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Domain.Errors.Entity;
using OnionArchitecture.Domain.Errors.Service;
using OnionArchitecture.Shared;

namespace OnionArchitecture.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;
    
    public TokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }
    
    public Result<string> CreateAccessToken(User? user)
    {
        if (user == null)
        {
            return Result<string>.Failure(UserErrors.NullOrEmpty);
        }
        
        //Get User's claims
        var claims = GetClaims(user);
        
        //Security key's symetric key
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));
        
        //Encrypted identity
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        
        JwtSecurityToken securityToken = new(
            audience: _jwtOptions.Audience,
            issuer: _jwtOptions.Issuer,
            expires: DateTime.UtcNow.AddSeconds(_jwtOptions.AccessTokenExpireTimeSecond),
            notBefore: DateTime.UtcNow,
            claims: claims,
            signingCredentials: signingCredentials
        );
        
        JwtSecurityTokenHandler tokenHandler = new();

        return Result<string>.Success(tokenHandler.WriteToken(securityToken));
        
    }

    public Result<string> GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Result<string>.Success(Convert.ToBase64String(randomNumber));
        }
    }

    public Result<string> GetUserIdFromJwtToken(string jwtToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var token = tokenHandler.ReadJwtToken(jwtToken);
        if (token == null)
        {
            return Result<string>.Failure(TokenErrors.InvalidToken);
        }
        
        var userIdClaim = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Result<string>.Failure(TokenErrors.UserIdClaimNotFound);
        }
        
        return Result<string>.Success(userIdClaim.Value);
    }

    private List<Claim> GetClaims(User user)
    {
        var authClaims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name.FirstName),
            new Claim(ClaimTypes.Surname, user.Name.LastName),
            new Claim(ClaimTypes.MobilePhone, user.Phone.Value),
            new Claim(ClaimTypes.Email, user.Email.Value)
        };

        foreach (var role in user.Roles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role.Title));
        }

        return authClaims;
    }
}