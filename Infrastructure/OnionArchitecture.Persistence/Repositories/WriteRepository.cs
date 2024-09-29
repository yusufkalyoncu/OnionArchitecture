using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnionArchitecture.Application.Abstractions.Repositories;
using OnionArchitecture.Shared;
using OnionArchitecture.Persistence.Contexts;

namespace OnionArchitecture.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : Entity
{
    private readonly OnionArchitectureDbContext _dbContext;

    public WriteRepository(OnionArchitectureDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DbSet<T> Table => _dbContext.Set<T>();
    
    public async Task<bool> AddAsync(T entity)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(entity);
        return entityEntry.State == EntityState.Added;
    }

    public async Task AddRangeAsync(List<T> entities)
    {
        await Table.AddRangeAsync(entities);
    }

    public bool Update(T entity)
    {
        EntityEntry<T> entityEntry = Table.Update(entity);
        return entityEntry.State == EntityState.Modified;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        T? entity = await Table.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
        if (entity == null) return false;
        return Delete(entity);
    }

    public bool Delete(T entity)
    {
        entity.Delete();
        return Update(entity);
    }

    public void DeleteRange(List<T> entities)
    {
        entities.ForEach(x => Delete(x));
    }

    public async Task<int> SaveAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}