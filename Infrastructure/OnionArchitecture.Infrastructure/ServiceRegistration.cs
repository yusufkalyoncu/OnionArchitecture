using Microsoft.Extensions.DependencyInjection;

namespace OnionArchitecture.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        var assembly = typeof(ServiceRegistration).Assembly;
    }
}