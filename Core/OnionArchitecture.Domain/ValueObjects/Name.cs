using OnionArchitecture.Domain.Errors.ValueObjects;
using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.Domain.ValueObjects;

public sealed class Name : ValueObject
{
    public const byte FirstNameMinLength = 2;
    public const byte FirstNameMaxLength = 30;
    public const byte LastNameMinLength = 2;
    public const byte LastNameMaxLength = 30;

    private Name(){}
    private Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        Value = $"{firstName} {lastName}";
    }
    public string? FirstName { get; }
    public string? LastName { get; }
    public string? Value { get; }
    
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value!;
    }

    public static Result<Name> Create(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName))
        {
            return Result<Name>.Failure(NameErrors.FirstNameNullOrEmpty);
        }
        if (string.IsNullOrEmpty(lastName))
        {
            return Result<Name>.Failure(NameErrors.LastNameNullOrEmpty);
        }
        if (firstName.Length < FirstNameMinLength || firstName.Length > FirstNameMaxLength)
        {
            return Result<Name>.Failure(NameErrors.FirstNameLengthError);
        }
        if (lastName.Length < LastNameMinLength || lastName.Length > LastNameMaxLength)
        {
            return Result<Name>.Failure(NameErrors.LastNameLengthError);
        }

        return Result<Name>.Success(new(firstName, lastName));
    }
}