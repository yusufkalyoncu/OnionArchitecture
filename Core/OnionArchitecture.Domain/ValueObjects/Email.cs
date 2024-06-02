using System.Text.RegularExpressions;
using OnionArchitecture.Domain.Errors.ValueObjects;
using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    private Email(){}
    private Email(string value) => Value = value;
    public string Value { get;}
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public Result Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure(EmailErrors.EmailNullOrEmpty);
        }
        
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
        {
            return Result.Failure(EmailErrors.InvalidFormat);
        }

        return Result.Success<Email>(new(value));
    }
}