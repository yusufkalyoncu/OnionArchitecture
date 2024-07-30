using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Application.Abstractions.Behaviors;
using OnionArchitecture.Application.DTOs.Token;
using OnionArchitecture.Application.Features.Auth.Commands.UserLogin;
using OnionArchitecture.Application.Features.Auth.Commands.UserRegister;
using OnionArchitecture.Application.Options;
using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.Application;

public static class ServiceRegistration
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ServiceRegistration).Assembly;
        
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            AddValidations(configuration);

        });
        services.AddValidatorsFromAssembly(assembly);
    }
    
    public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgreOptions>(configuration.GetSection(PostgreOptions.OptionKey));
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.OptionKey));
    }

    private static void AddValidations(MediatRServiceConfiguration configuration)
    {
        configuration.AddValidation<UserRegisterCommand, TokenDto>();
        configuration.AddValidation<UserLoginCommand, TokenDto>();
    }

    private static MediatRServiceConfiguration AddValidation<TRequest, TResponse>(
        this MediatRServiceConfiguration config)
        where TRequest : notnull
    {
        return config.AddBehavior<IPipelineBehavior<TRequest, Result<TResponse>>, ValidationPipelineBehavior<TRequest, TResponse>>();
    }
}