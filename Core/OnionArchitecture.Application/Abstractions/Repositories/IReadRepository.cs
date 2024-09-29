using System.Linq.Expressions;
using OnionArchitecture.Shared;

namespace OnionArchitecture.Application.Abstractions.Repositories;

public interface IReadRepository<T> : IRepository<T> where T : Entity
{
    IQueryable<T>? GetAll(bool tracking = true);
    IQueryable<T?> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
    Task<T?> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
    Task<T?> GetByIdAsync(Guid id, bool tracking = true);
}