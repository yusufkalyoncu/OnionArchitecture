using System.Net;

namespace OnionArchitecture.Shared;

public class BaseException : Exception
{
    public HttpStatusCode Status { get; private set; }
    public string Type { get; private set; }
    public string? ErrorCode { get; private set; }
    public object? Parameters { get; private set; }

    protected BaseException(
        HttpStatusCode status,
        string type,
        string? message,
        object? parameters = null,
        string? errorCode = null)
        : base(message)
    {
        Type = type;
        Status = status;
        Parameters = parameters;
        ErrorCode = errorCode;
    }
}