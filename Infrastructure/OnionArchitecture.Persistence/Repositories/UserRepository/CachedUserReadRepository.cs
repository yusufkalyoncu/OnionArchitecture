using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnionArchitecture.Application.Abstractions.Repositories.UserRepository;
using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Persistence.Repositories.UserRepository;

public class CachedUserReadRepository : IUserReadRepository
{
    private readonly UserReadRepository _decorated;
    private readonly IMemoryCache _memoryCache;
    
    public CachedUserReadRepository(
        UserReadRepository decorated,
        IMemoryCache memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }
    
    public DbSet<User> Table { get; }
    
    public IQueryable<User>? GetAll(bool tracking = true)
    {
        string key = "user-get-all";

        if (!_memoryCache.TryGetValue(key, out IQueryable<User>? users))
        {
            users = _decorated.GetAll();
            if(users != null) _memoryCache.Set(key, users,TimeSpan.FromMinutes(2));
        }

        return users;
    }

    public IQueryable<User?> GetWhere(Expression<Func<User, bool>> method, bool tracking = true)
    {
        return _decorated.GetWhere(method, tracking);
    }

    public async Task<User?> GetSingleAsync(Expression<Func<User, bool>> method, bool tracking = true)
    {
        return await _decorated.GetSingleAsync(method, tracking);
    }

    public async Task<User?> GetByIdAsync(string id, bool tracking = true)
    {
        string key = $"user-get-by-id-{id.ToString()}";

        if (!_memoryCache.TryGetValue(key, out User? user))
        {
            user = await _decorated.GetByIdAsync(id, tracking);
            if (user != null) _memoryCache.Set(key, user, TimeSpan.FromMinutes(5));
        }

        return user;
    }

    public async Task<User?> GetByIdWithRolesAsync(Guid userId)
    {
        string key = $"user-get-by-id-with-roles-{userId.ToString()}";

        if (!_memoryCache.TryGetValue(key, out User? user))
        {
            user = await _decorated.GetByIdWithRolesAsync(userId);
            if (user != null) _memoryCache.Set(key, user, TimeSpan.FromMinutes(5));
        }

        return user;
    }

    public async Task<User?> GetByPhoneAsync(string phone)
    {
        string key = $"user-get-by-phone-{phone}";
        
        if (!_memoryCache.TryGetValue(key, out User? user))
        {
            user = await _decorated.GetByPhoneAsync(phone);
            if (user != null) _memoryCache.Set(key, user, TimeSpan.FromMinutes(5));
        }

        return user;
    }

    public async Task<User?> GetByPhoneOrEmailAsync(string phone, string email)
    {
        string key = $"user-get-by-phone-and-email-{phone}-{email}";

        if (!_memoryCache.TryGetValue(key, out User? user))
        {
            user = await _decorated.GetByPhoneOrEmailAsync(phone, email);
            if (user != null) _memoryCache.Set(key, user, TimeSpan.FromMinutes(5));
        }

        return user;
    }

    public async Task<User?> GetByPhoneOrEmailAsync(string phoneOrEmail)
    {
        string key = $"user-get-by-phone-or-email-{phoneOrEmail}";

        if (!_memoryCache.TryGetValue(key, out User? user))
        {
            user = await _decorated.GetByPhoneOrEmailAsync(phoneOrEmail);
            if (user != null) _memoryCache.Set(key, user, TimeSpan.FromMinutes(5));
        }

        return user;
    }
}