using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Application.Abstractions.Behaviors;
using OnionArchitecture.Application.Options;

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
        });
        services.AddValidatorsFromAssembly(assembly);
    }
    
    public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgreOptions>(configuration.GetSection(PostgreOptions.OptionKey));
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.OptionKey));
    }
}