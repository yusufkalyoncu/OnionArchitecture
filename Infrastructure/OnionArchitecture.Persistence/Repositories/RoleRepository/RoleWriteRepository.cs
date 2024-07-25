using OnionArchitecture.Application.Abstractions.Repositories.RoleRepository;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Persistence.Contexts;

namespace OnionArchitecture.Persistence.Repositories.RoleRepository;

public class RoleWriteRepository : WriteRepository<Role>, IRoleWriteRepository
{
    public RoleWriteRepository(OnionArchitectureDbContext dbContext) : base(dbContext)
    {
    }
}