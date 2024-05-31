using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.Domain.Errors.ValueObjects;

public static class EmailErrors
{
    public static readonly Error EmailNullOrEmpty = Error.NullOrEmpty("Email cannot be null or empty");
    public static readonly Error InvalidFormat = Error.Validation("Invalid email format");
}