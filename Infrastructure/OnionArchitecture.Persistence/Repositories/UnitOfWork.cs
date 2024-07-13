using Microsoft.EntityFrameworkCore.Storage;
using OnionArchitecture.Application.Abstractions.Repositories;
using OnionArchitecture.Persistence.Contexts;

namespace OnionArchitecture.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly OnionArchitectureDbContext _context;
    private IDbContextTransaction? _transaction;
    
    public UnitOfWork(OnionArchitectureDbContext context, IDbContextTransaction? transaction)
    {
        _context = context;
        _transaction = transaction;
    }
    
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
        return _transaction;
    }

    public async Task CommitAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    
    public async ValueTask DisposeAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
        await _context.DisposeAsync();
    }
}