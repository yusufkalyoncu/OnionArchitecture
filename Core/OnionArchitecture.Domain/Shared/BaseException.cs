using System.Net;

namespace OnionArchitecture.Domain.Shared;

public class BaseException : Exception
{
    public string Type { get; set; }
    public object Parameters { get; set; }
    public HttpStatusCode Status { get; set; }
    
    public BaseException(HttpStatusCode status,string type,string? message,  object parameters = null!) : base(message)
    {
        Type = type;
        Status = status;
        Parameters = parameters;
    }
}