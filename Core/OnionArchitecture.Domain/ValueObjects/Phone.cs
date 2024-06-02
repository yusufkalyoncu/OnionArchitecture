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
        Value = $"{countryCode}{number}";
    }
    public string CountryCode { get; }
    public string Number { get;}
    public string Value { get;}
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public Result Create(string countryCode, string number)
    {
        if (string.IsNullOrEmpty(countryCode))
        {
            return Result.Failure(PhoneErrors.CountryCodeNullOrEmpty);
        }
        if (string.IsNullOrEmpty(number))
        {
            return Result.Failure(PhoneErrors.NumberNullOrEmpty);
        }
        
        string notDigitPattern = @"\D";
        string countryCodePattern = @"^\d{1,3}$";
        string numberPattern = @"^\d{10,15}$";

        countryCode = Regex.Replace(countryCode, notDigitPattern, "");
        number = Regex.Replace(number, notDigitPattern, "");

        if (!Regex.IsMatch(countryCode, countryCodePattern))
        {
            return Result.Failure(PhoneErrors.InvalidCountryCode);
        }
        if (!Regex.IsMatch(number, numberPattern))
        {
            return Result.Failure(PhoneErrors.InvalidCountryCode);
        }

        return Result.Success<Phone>(new(countryCode, number));
    }
}