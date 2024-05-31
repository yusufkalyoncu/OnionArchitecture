using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.Domain.Errors.ValueObjects;

public static class NameErrors
{
    public static readonly Error FirstNameNullOrEmpty = Error.NullOrEmpty("First name cannot be null or empty");
    public static readonly Error LastNameNullOrEmpty = Error.NullOrEmpty("Last name cannot be null or empty");
    public static readonly Error FirstNameLengthError = Error.Validation("Name's length must be 2-20");
    public static readonly Error LastNameLengthError = Error.Validation("Name's length must be 2-20");
}