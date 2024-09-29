
using OnionArchitecture.Shared;

namespace OnionArchitecture.Domain.Errors.Entity;

public class RoleErrors
{
    public static readonly Error DefaultRoleNotFound = Error.NotFound("Default role not found");
}