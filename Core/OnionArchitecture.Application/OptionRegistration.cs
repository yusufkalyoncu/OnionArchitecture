using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Application.Options;

namespace OnionArchitecture.Application;

public static class OptionRegistration
{
    public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgreOptions>(configuration.GetSection(PostgreOptions.OptionKey));
    }
}