using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.Domain.Errors.ValueObjects;

public static class PhoneErrors
{
    public static readonly Error CountryCodeNullOrEmpty = Error.NullOrEmpty("Country code cannot be null or empty");
    public static readonly Error NumberNullOrEmpty = Error.NullOrEmpty("Number cannot be null or empty");
    public static readonly Error InvalidCountryCode = Error.Validation("Invalid country code");
    public static readonly Error InvalidNumber = Error.Validation("Invalid phone number");
    public static readonly Error ShouldBeStartsFive = Error.Validation("Phone number should be starts with 5");
}