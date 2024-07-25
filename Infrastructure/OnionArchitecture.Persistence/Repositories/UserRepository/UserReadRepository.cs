using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Application.Abstractions.Repositories.UserRepository;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Persistence.Contexts;

namespace OnionArchitecture.Persistence.Repositories.UserRepository;

public class UserReadRepository : ReadRepository<User>, IUserReadRepository
{
    public UserReadRepository(OnionArchitectureDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetByIdWithRolesAsync(Guid userId)
    {
        return await Table
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);
    }

    public async Task<User?> GetByPhoneAsync(string phone)
    {
        return await Table
            .FirstOrDefaultAsync(x => !x.IsDeleted && x.Phone.CountryCode + x.Phone.Number == phone);
    }

    public async Task<User?> GetByPhoneOrEmailAsync(string phone, string email)
    {
        return await Table
            .FirstOrDefaultAsync(x => !x.IsDeleted && (x.Phone.CountryCode + x.Phone.Number == phone || x.Email.Value == email));
    }

    public async Task<User?> GetByPhoneOrEmailAsync(string phoneOrEmail)
    {
        return await Table
            .Include(u => u.Roles)
            .FirstOrDefaultAsync(x =>
                !x.IsDeleted && (x.Phone.CountryCode + x.Phone.Number == phoneOrEmail ||
                                 x.Email.Value == phoneOrEmail));
    }
}