using System.Text.RegularExpressions;
using OnionArchitecture.Domain.Errors.ValueObjects;
using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public const string RegexPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    
    private Email(){}
    private Email(string value) => Value = value;
    public string Value { get;}
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result<Email>.Failure(EmailErrors.EmailNullOrEmpty);
        }
        
        if (!Regex.IsMatch(value, RegexPattern, RegexOptions.IgnoreCase))
        {
            return Result<Email>.Failure(EmailErrors.InvalidFormat);
        }

        return Result<Email>.Success(new(value));
    }
}