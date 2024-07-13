using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.Application.Abstractions.Repositories;

public interface IRepository<T> where T : Entity
{
    DbSet<T> Table { get; }
}