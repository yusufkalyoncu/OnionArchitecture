using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OnionArchitecture.Application.Abstractions.Services;
using OnionArchitecture.Infrastructure.Services;

namespace OnionArchitecture.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(ServiceRegistration).Assembly;

        var jwtOptions = configuration.GetSection(JwtOptions.OptionKey).Get<JwtOptions>()!;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                
                ValidAudience = jwtOptions.Audience,
                ValidIssuer = jwtOptions.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey)),
                LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                    expires != null ? expires > DateTime.UtcNow : false
            };
        });
    }

    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<ITokenService, TokenService>();
    }
}