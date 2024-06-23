using System.Net;

namespace OnionArchitecture.Domain.Shared;

public class BaseException : Exception
{
    public string Error { get; set; }
    public object Parameters { get; set; }
    public HttpStatusCode Status { get; set; }
    
    public BaseException(HttpStatusCode status,string error,string? message,  object parameters = null!) : base(message)
    {
        Error = error;
        Status = status;
        Parameters = parameters;
    }
}