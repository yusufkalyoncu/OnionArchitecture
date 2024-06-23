using System.Net;

namespace OnionArchitecture.Domain.Shared;

public sealed class Error
{
    public int Status { get; private set; }
    public string Title { get; private set; }
    public string Message { get; private set; }
    public string? Code { get; private set; }

    private Error(int status, string title, string message, string? code)
    {
        Status = status;
        Title = title;
        Message = message;
        Code = code;
    }
    
    public static Error BadRequest(string message, string? code = null) =>
        new(400, "Bad Request", message, code);
    public static Error Unauthorized(string message, string? code = null) =>
        new(401, "Unauthorized", message, code);
    public static Error NotFound(string message, string? code = null) =>
        new(404, "Not Found",  message, code);
    public static Error Conflict(string message, string? code = null) =>
        new(409, "Conflict", message, code);
    public static Error Server(string message, string? code = null) =>
        new(500, "Internal Server Error", message, code);
    
    
    public static Error Validation(string message, string? code = null) =>
        new(400, "Validation Error", message, code);
    public static Error NullOrEmpty(string message, string? code = null) =>
        new(400, "Null or Empty Error", message, code);
    public static Error NullValue => 
        new(400, "Bad Request", "Value cannot be null", null);
}