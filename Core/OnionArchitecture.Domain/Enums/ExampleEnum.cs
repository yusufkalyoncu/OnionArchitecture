using OnionArchitecture.Shared;

namespace OnionArchitecture.Domain.Enums;

public class ExampleEnum : Enumeration
{
    public static ExampleEnum Example = new(0, nameof(Example));
    public ExampleEnum(int value, string name) : base(value, name)
    {
    }
}