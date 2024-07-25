using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Abstractions.Repositories.RoleRepository;

public interface IRoleReadRepository : IReadRepository<Role>
{
    public Task<Role?> GetDefaultRoleAsync();
    public Task<Role?> GetByTitleAsync(string title);
}