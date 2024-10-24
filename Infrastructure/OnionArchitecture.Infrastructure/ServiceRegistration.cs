using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OnionArchitecture.Application.Abstractions.Services;
using OnionArchitecture.Infrastructure.Extensions;
using OnionArchitecture.Infrastructure.Jobs;
using OnionArchitecture.Infrastructure.Services;
using Quartz;

namespace OnionArchitecture.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwtAuthentication(configuration);
        services.AddApplicationServices();
        services.UseCronJobs(configuration);
    }
    
    private static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
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
    
    private static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<ITokenService, TokenService>();
    }
    
    private static void UseCronJobs(this IServiceCollection services, IConfiguration configuration)
    {
        var cronExpressionOptions = configuration
            .GetSection(CronExpressionOptions.OptionKey)
            .Get<CronExpressionOptions>()!;
        
        services.AddQuartz(q =>
        {
            q.AddCronJob<ExampleJob>(nameof(ExampleJob), cronExpressionOptions.ExampleJob);
        });
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
    }
}