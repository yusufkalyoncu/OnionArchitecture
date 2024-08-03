using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OnionArchitecture.Persistence.Contexts;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OnionArchitectureDbContext>
{
    public OnionArchitectureDbContext CreateDbContext(string[] args)
    {

        DbContextOptionsBuilder<OnionArchitectureDbContext> dbContextBuilder = new();
        dbContextBuilder.UseNpgsql(
            "User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=OnionArchitecture;");
        return new(dbContextBuilder.Options);
    }
}