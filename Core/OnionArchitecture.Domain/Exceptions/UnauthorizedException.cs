using System.Net;
using OnionArchitecture.Shared;

namespace OnionArchitecture.Domain.Exceptions;

public class UnauthorizedException : BaseException
{
    public UnauthorizedException(
        string? message,
        object parameters = null) 
        : base(HttpStatusCode.Unauthorized, "UNAUTHORIZED", message, parameters)
    {
    }
}