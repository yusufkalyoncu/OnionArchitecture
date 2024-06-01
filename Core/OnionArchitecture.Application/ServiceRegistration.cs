using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Application.Abstractions.Behaviors;

namespace OnionArchitecture.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ServiceRegistration).Assembly;
        
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
        });
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}