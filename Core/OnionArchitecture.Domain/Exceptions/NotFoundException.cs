using System.Net;
using OnionArchitecture.Shared;

namespace OnionArchitecture.Domain.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(
        string? message,
        object parameters = null) 
        : base(HttpStatusCode.NotFound, "NOT_FOUND", message, parameters)
    {
    }
}