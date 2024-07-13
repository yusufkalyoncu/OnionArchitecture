using Microsoft.EntityFrameworkCore.Storage;

namespace OnionArchitecture.Application.Abstractions.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task CommitAsync();
}