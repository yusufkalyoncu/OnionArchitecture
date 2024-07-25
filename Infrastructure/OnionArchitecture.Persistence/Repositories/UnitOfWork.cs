using OnionArchitecture.Application.Abstractions.Repositories;
using OnionArchitecture.Application.Abstractions.Repositories.RoleRepository;
using OnionArchitecture.Application.Abstractions.Repositories.UserRepository;
using OnionArchitecture.Persistence.Contexts;
using OnionArchitecture.Persistence.Repositories.RoleRepository;
using OnionArchitecture.Persistence.Repositories.UserRepository;

namespace OnionArchitecture.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly OnionArchitectureDbContext _context;
    
    private IUserReadRepository? _userReadRepository;
    private IUserWriteRepository? _userWriteRepository;

    private IRoleReadRepository? _roleReadRepository;
    private IRoleWriteRepository? _roleWriteRepository;
    public UnitOfWork(OnionArchitectureDbContext context)
    {
        _context = context;
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
                _userReadRepository = new UserReadRepository(_context);
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