using OnionArchitecture.Application.Abstractions.Repositories.UserRepository;
using OnionArchitecture.Persistence.Contexts;
using OnionArchitecture.Persistence.Repositories.UserRepository;
using Microsoft.Extensions.Caching.Memory;
using OnionArchitecture.Application.Abstractions.Repositories;
using OnionArchitecture.Application.Abstractions.Repositories.RoleRepository;
using OnionArchitecture.Persistence.Repositories.RoleRepository;

namespace OnionArchitecture.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly OnionArchitectureDbContext _context;
    private readonly IMemoryCache _memoryCache;
    
    private IUserReadRepository? _userReadRepository;
    private IUserWriteRepository? _userWriteRepository;

    private IRoleReadRepository? _roleReadRepository;
    private IRoleWriteRepository? _roleWriteRepository;
    
    public UnitOfWork(OnionArchitectureDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
    
    public IUserReadRepository UserReadRepository
    {
        get
        {
            if (_userReadRepository is null)
            {
                // Burada CachedUserReadRepository'yi kullanıyoruz
                var userReadRepository = new UserReadRepository(_context);
                _userReadRepository = new CachedUserReadRepository(userReadRepository, _memoryCache);
            }
            return _userReadRepository;
        }
    }

    public IUserWriteRepository UserWriteRepository
    {
        get
        {
            if (_userWriteRepository is null)
            {
                _userWriteRepository = new UserWriteRepository(_context);
            }
            return _userWriteRepository;
        }
    }

    public IRoleReadRepository RoleReadRepository
    {
        get
        {
            if (_roleReadRepository is null)
            {
                _roleReadRepository = new RoleReadRepository(_context);
            }

            return _roleReadRepository;
        }
    }
    
    public IRoleWriteRepository RoleWriteRepository
    {
        get
        {
            if (_roleWriteRepository is null)
            {
                _roleWriteRepository = new RoleWriteRepository(_context);
            }

            return _roleWriteRepository;
        }
    }
}