using OnionArchitecture.Application.Abstractions.Repositories.RoleRepository;
using OnionArchitecture.Application.Abstractions.Repositories.UserRepository;

namespace OnionArchitecture.Application.Abstractions.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserReadRepository UserReadRepository { get; }
    IUserWriteRepository UserWriteRepository { get; }
    IRoleReadRepository RoleReadRepository { get; }
    IRoleWriteRepository RoleWriteRepository { get; }
    Task<int> CompleteAsync();
}