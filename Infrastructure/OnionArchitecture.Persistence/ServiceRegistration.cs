using Microsoft.Extensions.DependencyInjection;
using OnionArchitecture.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnionArchitecture.Application.Abstractions.Repositories;
using OnionArchitecture.Application.Abstractions.Repositories.RoleRepository;
using OnionArchitecture.Application.Abstractions.Repositories.UserRepository;
using OnionArchitecture.Application.Abstractions.Services;
using OnionArchitecture.Application.Options;
using OnionArchitecture.Persistence.Repositories;
using OnionArchitecture.Persistence.Repositories.RoleRepository;
using OnionArchitecture.Persistence.Repositories.UserRepository;
using OnionArchitecture.Persistence.Seeds;
using OnionArchitecture.Persistence.Services;

namespace OnionArchitecture.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgreSqlDbContext(configuration);
        services.AddRepositoryServices();
        services.AddApplicationServices();
    }
    private static void AddPostgreSqlDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var postgreOptions = configuration.GetSection(PostgreOptions.OptionKey).Get<PostgreOptions>()!;
        services.AddDbContext<OnionArchitectureDbContext>(
            options => options.UseNpgsql(postgreOptions?.ConnectionString));
    }
    
    private static void AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddTransient<UserReadRepository>();
        services.AddTransient<IUserReadRepository, CachedUserReadRepository>();
        services.AddTransient<IUserWriteRepository, UserWriteRepository>();
        
        services.AddTransient<RoleReadRepository>();
        services.AddTransient<IRoleReadRepository, CachedRoleReadRepository>();
        services.AddTransient<IRoleWriteRepository, RoleWriteRepository>();
    }
    
    public static void AddSeeds(this IServiceProvider services)
    {
        try
        {
            RoleSeed.Seed(services).Wait();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
    
    private static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
    }
}
