using System.Text.RegularExpressions;
using OnionArchitecture.Domain.Errors.ValueObjects;
using OnionArchitecture.Shared;

namespace OnionArchitecture.Domain.ValueObjects;

public sealed class Phone : ValueObject
{
    public const string NotDigitPattern = @"\D";
    public const string CountryCodePattern = @"^\d{2}$";
    public const string NumberPattern = @"^\d{10}$";
    public const string NumberStartsWith5Pattern = @"^5";
    
    private Phone(){}
    private Phone(string countryCode, string number)
    {
        CountryCode = countryCode;
        Number = number;
    }
    public string CountryCode { get; }
    public string Number { get;}

    public string Value
    {
        get
        {
            return $"{CountryCode}{Number}";
        }
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<Phone> Create(string countryCode, string number)
    {
        if (string.IsNullOrEmpty(countryCode))
        {
            return Result<Phone>.Failure(PhoneErrors.CountryCodeNullOrEmpty);
        }
        if (string.IsNullOrEmpty(number))
        {
            return Result<Phone>.Failure(PhoneErrors.NumberNullOrEmpty);
        }

        countryCode = Regex.Replace(countryCode, NotDigitPattern, "");
        number = Regex.Replace(number, NotDigitPattern, "");

        if (!Regex.IsMatch(countryCode, CountryCodePattern))
        {
            return Result<Phone>.Failure(PhoneErrors.InvalidCountryCode);
        }
        if (!Regex.IsMatch(number, NumberPattern))
        {
            return Result<Phone>.Failure(PhoneErrors.InvalidNumber);
        }

        if (!Regex.IsMatch(number, NumberStartsWith5Pattern))
        {
            return Result<Phone>.Failure(PhoneErrors.ShouldBeStartsFive);
        }

        return Result<Phone>.Success(new(countryCode, number));
    }
}