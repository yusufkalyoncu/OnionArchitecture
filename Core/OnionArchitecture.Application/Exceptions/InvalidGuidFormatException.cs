using OnionArchitecture.Domain.Exceptions;

namespace OnionArchitecture.Application.Exceptions;

public class InvalidGuidFormatException : BadRequestException
{
    public InvalidGuidFormatException(object parameters = null)
        : base("Invalid Guid Format", parameters)
    {
    }
}