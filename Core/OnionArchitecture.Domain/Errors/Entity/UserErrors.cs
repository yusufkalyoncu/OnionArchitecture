
using OnionArchitecture.Shared;

namespace OnionArchitecture.Domain.Errors.Entity;

public class UserErrors
{
    public static readonly Error PasswordNullOrEmpty = Error.NullOrEmpty("Password cannot be null or empty");
    public static readonly Error NullOrEmpty = Error.NullOrEmpty("User cannot be null or empty");
    public static readonly Error PasswordDoesntMatch = Error.BadRequest("Passwords doesnt matched");
    public static readonly Error PasswordInvalidLength = Error.BadRequest("Password's length is invalid");
    public static readonly Error AlreadyExistsPhone = Error.Conflict("A user with this phone number already exists");
    public static readonly Error AlreadyExistsEmail = Error.Conflict("A user with this email already exists");
    public static readonly Error AlreadyExists = Error.Conflict("A user with these details already exists");
    public static readonly Error RoleAlreadyExists = Error.Conflict("User already has this role");
    public static readonly Error NotFound = Error.NotFound("User not found");
    public static readonly Error WrongUsernameOrPassword =
        Error.Unauthorized("Login failed. Please check your username and password");

    public static readonly Error SessionExpired = Error.Unauthorized("Session expired. Please login again.");
}