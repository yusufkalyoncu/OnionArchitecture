using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnionArchitecture.Application.Options;

namespace OnionArchitecture.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var postgreOptions = configuration.GetSection(PostgreOptions.OptionKey).Get<PostgreOptions>()!;
        services.AddDbContext<OnionArchitectureDbContext>(
            options => options.UseNpgsql(postgreOptions?.ConnectionString));
    }
}