using OnionArchitecture.Application.Abstractions.Repositories.UserRepository;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Persistence.Contexts;

namespace OnionArchitecture.Persistence.Repositories.UserRepository;

public class UserWriteRepository : WriteRepository<User>, IUserWriteRepository
{
    public UserWriteRepository(OnionArchitectureDbContext dbContext) : base(dbContext)
    {
    }
}