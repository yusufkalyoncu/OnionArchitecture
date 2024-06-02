using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Domain.ValueObjects;
using OnionArchitecture.Persistence.Configurations;

namespace OnionArchitecture.Persistence.Contexts;

public class OnionArchitectureDbContext : DbContext
{
    public OnionArchitectureDbContext() : base(){}
    public OnionArchitectureDbContext(DbContextOptions options): base(options){}

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}