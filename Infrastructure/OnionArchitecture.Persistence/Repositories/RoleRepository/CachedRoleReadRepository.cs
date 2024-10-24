using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnionArchitecture.Application.Abstractions.Repositories.RoleRepository;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Persistence.Repositories.RoleRepository;

public class CachedRoleReadRepository : IRoleReadRepository
{
    private readonly RoleReadRepository _decorated;
    private readonly IMemoryCache _memoryCache;

    public CachedRoleReadRepository(
        RoleReadRepository decorated,
        IMemoryCache memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public DbSet<Role> Table { get; }
    
    public IQueryable<Role>? GetAll(bool tracking = true)
    {
        string key = "role-get-all";

        if (!_memoryCache.TryGetValue(key, out IQueryable<Role>? roles))
        {
            roles = _decorated.GetAll();
            if (roles != null) _memoryCache.Set(key, roles);
        }

        return roles;
    }

    public IQueryable<Role?> GetWhere(Expression<Func<Role, bool>> method, bool tracking = true)
    {
        return _decorated.GetWhere(method, tracking);
    }

    public async Task<Role?> GetSingleAsync(Expression<Func<Role, bool>> method, bool tracking = true)
    {
        return await _decorated.GetSingleAsync(method, tracking);
    }

    public async Task<Role?> GetByIdAsync(string id, bool tracking = true)
    {
        string key = $"role-get-by-id-{id}";

        if (!_memoryCache.TryGetValue(key, out Role? role))
        {
            role = await _decorated.GetByIdAsync(id, tracking);
            if(role != null) _memoryCache.Set(key, role);
        }

        return role;
    }

    public async Task<Role?> GetDefaultRoleAsync()
    {
        string key = "role-get-default";

        if (!_memoryCache.TryGetValue(key, out Role? role))
        {
            role = await _decorated.GetDefaultRoleAsync();
            if(role != null) _memoryCache.Set(key, role);
        }

        return role;
    }

    public async Task<Role?> GetByTitleAsync(string title)
    {
        string key = $"role-get-by-title-{title}";

        if (!_memoryCache.TryGetValue(key, out Role? role))
        {
            role = await _decorated.GetByTitleAsync(title);
            if(role != null) _memoryCache.Set(key, role);
        }

        return role;
    }
}