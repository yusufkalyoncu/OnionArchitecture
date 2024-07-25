using OnionArchitecture.Domain.Entities;

namespace OnionArchitecture.Application.Abstractions.Repositories.UserRepository;

public interface IUserReadRepository : IReadRepository<User>
{
    public Task<User?> GetByIdWithRolesAsync(Guid userId);
    public Task<User?> GetByPhoneAsync(string phone);
    public Task<User?> GetByPhoneOrEmailAsync(string phone, string email);
    public Task<User?> GetByPhoneOrEmailAsync(string phoneOrEmail);
}