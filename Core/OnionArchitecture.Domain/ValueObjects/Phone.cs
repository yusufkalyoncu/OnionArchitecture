using System.Text.RegularExpressions;
using OnionArchitecture.Domain.Errors.ValueObjects;
using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.Domain.ValueObjects;

public sealed class Phone : ValueObject
{
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
        
        string notDigitPattern = @"\D";
        string countryCodePattern = @"^\d{1,3}$";
        string numberPattern = @"^\d{10,15}$";

        countryCode = Regex.Replace(countryCode, notDigitPattern, "");
        number = Regex.Replace(number, notDigitPattern, "");

        if (!Regex.IsMatch(countryCode, countryCodePattern))
        {
            return Result<Phone>.Failure(PhoneErrors.InvalidCountryCode);
        }
        if (!Regex.IsMatch(number, numberPattern))
        {
            return Result<Phone>.Failure(PhoneErrors.InvalidCountryCode);
        }

        return Result<Phone>.Success(new(countryCode, number));
    }
}