using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.Application.Abstractions.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : Entity
{
    Task<bool> AddAsync(T entity);
    Task AddRangeAsync(List<T> entities);
    bool Update(T entity);
    Task<bool> DeleteAsync(Guid id);
    bool Delete(T entity);
    void DeleteRange(List<T> entities);
    Task<int> SaveAsync();
}