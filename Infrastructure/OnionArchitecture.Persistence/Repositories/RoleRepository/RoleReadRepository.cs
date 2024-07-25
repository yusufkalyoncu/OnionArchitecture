using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Application.Abstractions.Repositories.RoleRepository;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Persistence.Contexts;

namespace OnionArchitecture.Persistence.Repositories.RoleRepository;

public class RoleReadRepository : ReadRepository<Role>, IRoleReadRepository
{
    public RoleReadRepository(OnionArchitectureDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Role?> GetDefaultRoleAsync()
    {
        return await Table
            .FirstOrDefaultAsync(x => !x.IsDeleted && x.IsDefault);
    }

    public async Task<Role?> GetByTitleAsync(string title)
    {
        return await Table
            .FirstOrDefaultAsync(x => x.Title == title && !x.IsDeleted);
    }
}