using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Application.Abstractions.Behaviors;
using OnionArchitecture.Application.DTOs.Token;
using OnionArchitecture.Application.Features.Auth.Commands.RefreshToken;
using OnionArchitecture.Application.Features.Auth.Commands.UserLogin;
using OnionArchitecture.Application.Features.Auth.Commands.UserRegister;
using OnionArchitecture.Application.Options;
using OnionArchitecture.Shared;

namespace OnionArchitecture.Application;

public static class ServiceRegistration
{
    public static Assembly assembly { get; private set; }
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        assembly = typeof(ServiceRegistration).Assembly;
        
        services.AddValidatorsFromAssembly(assembly);
        AddMediatR(services);
        AddOptions(services, configuration);
    }

    private static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            AddValidations(configuration);

        });
    }
    
    private static void AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgreOptions>(configuration.GetSection(PostgreOptions.OptionKey));
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.OptionKey));
    }

    private static void AddValidations(MediatRServiceConfiguration configuration)
    {
        configuration.AddValidation<UserRegisterCommand, TokenDto>();
        configuration.AddValidation<UserLoginCommand, TokenDto>();
        configuration.AddValidation<RefreshTokenCommand, TokenDto>();
    }

    private static MediatRServiceConfiguration AddValidation<TRequest, TResponse>(
        this MediatRServiceConfiguration config)
        where TRequest : notnull
    {
        return config.AddBehavior<IPipelineBehavior<TRequest, Result<TResponse>>, ValidationPipelineBehavior<TRequest, TResponse>>();
    }
}