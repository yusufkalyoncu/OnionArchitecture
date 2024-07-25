using System.Net;
using OnionArchitecture.Domain.Shared;

namespace OnionArchitecture.Domain.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(
        string? message,
        object parameters = null) 
        : base(HttpStatusCode.BadRequest, "BAD_REQUEST", message, parameters)
    {
    }
}