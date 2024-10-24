using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Application.Abstractions.Behaviors;
using OnionArchitecture.Application.Options;

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
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
        });
    }
    
    private static void AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgreOptions>(configuration.GetSection(PostgreOptions.OptionKey));
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.OptionKey));
    }
}