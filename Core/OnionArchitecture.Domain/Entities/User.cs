using OnionArchitecture.Domain.Shared;
using OnionArchitecture.Domain.ValueObjects;

namespace OnionArchitecture.Domain.Entities;

public sealed class User : Entity
{
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
}