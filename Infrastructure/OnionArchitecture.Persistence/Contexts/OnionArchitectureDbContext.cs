using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Domain.Shared;
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

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var datas = ChangeTracker.Entries<Entity>();

        foreach (var data in datas)
        {
            switch (data.State)
            {
                case EntityState.Added:
                    data.Entity.SetCreatedAt();
                    break;
                case EntityState.Modified:
                    data.Entity.Update();
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}