using OnionArchitecture.Domain.Errors.Entity;
using OnionArchitecture.Shared;
using OnionArchitecture.Domain.ValueObjects;

namespace OnionArchitecture.Domain.Entities;

public sealed class User : Entity
{
    public const byte PasswordMinLength = 6;
    public const byte PasswordMaxLength = 30;
    
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
        string plainTextPassword,
        string hashedPassword)
    {
        if (string.IsNullOrEmpty(plainTextPassword) || string.IsNullOrEmpty(hashedPassword))
        {
            return Result<User>.Failure(UserErrors.PasswordNullOrEmpty);
        }

        if (plainTextPassword.Length < PasswordMinLength || plainTextPassword.Length > PasswordMaxLength)
        {
            return Result<User>.Failure(UserErrors.PasswordInvalidLength);
        }
        
        return Result<User>.Success(new(name, email, phone, hashedPassword));
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