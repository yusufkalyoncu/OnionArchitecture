using OnionArchitecture.Shared;

namespace OnionArchitecture.Domain.Entities;

public sealed class Role : Entity
{
    private Role(){}
    public string Title { get; set; }
    public ICollection<User> Users { get; set; }
    public bool IsDefault { get; set; }

    public Role(string title, bool isDefault)
    {
        Title = title;
        IsDefault = isDefault;
    }
}