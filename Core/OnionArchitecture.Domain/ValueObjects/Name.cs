using OnionArchitecture.Domain.Errors.ValueObjects;
using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.Domain.ValueObjects;

public sealed class Name : ValueObject
{
    private Name(){}
    private Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        Value = $"{firstName} {lastName}";
    }
    public string FirstName { get; }
    public string LastName { get; }
    public string Value { get; }
    
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public Result<Name> Create(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName))
        {
            return Result<Name>.Failure(NameErrors.FirstNameNullOrEmpty);
        }
        if (string.IsNullOrEmpty(lastName))
        {
            return Result<Name>.Failure(NameErrors.LastNameNullOrEmpty);
        }
        if (firstName.Length < 2 || firstName.Length > 20)
        {
            return Result<Name>.Failure(NameErrors.FirstNameLengthError);
        }
        if (lastName.Length < 2 || firstName.Length > 20)
        {
            return Result<Name>.Failure(NameErrors.LastNameLengthError);
        }

        return Result<Name>.Success(new(firstName, lastName));
    }
}