using OnionArchitecture.Domain.Errors.Entity;
using OnionArchitecture.Domain.Shared;
using OnionArchitecture.Domain.ValueObjects;

namespace OnionArchitecture.Domain.Entities;

public sealed class User : Entity
{
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
    public string Password { get; private set; }
    public string RefreshToken { get; private set; }
    public DateTime RefreshTokenExpireAt { get; private set; }
    public DateTime? LastLoginTimestamp { get; private set; }
    public ICollection<Role> Roles { get; private set; }

    private User(){}
    private User(
        Name name,
        Email email,
        Phone phone,
        string password)
    {
        Name = name;
        Email = email;
        Phone = phone;
        Password = password;
        Roles = new List<Role>();
    }

    public static Result<User> Create(
        Name name,
        Email email,
        Phone phone,
        string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return Result<User>.Failure(UserErrors.PasswordNullOrEmpty);
        }
        
        return Result<User>.Success(new(name, email, phone, password));
    }

    public void UpdateLastLoginDate()
    {
        LastLoginTimestamp = DateTime.UtcNow;
    }
    
    public Result AddRole(Role role)
    {
        if (Roles.Any(r => r.Title == role.Title))
        {
            return Result.Failure(UserErrors.RoleAlreadyExists);
        }

        Roles.Add(role);
        return Result.Success();
    }

    public void UpdateRefreshToken(string refreshToken, int expiresSecond)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpireAt = DateTime.UtcNow.AddSeconds(expiresSecond);
    }
}