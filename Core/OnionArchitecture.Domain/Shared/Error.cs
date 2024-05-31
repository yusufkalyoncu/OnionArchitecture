using System.Net;

namespace OnionArchitecture.Domain.Shared;

public record Error
{
    public int Status { get; private set; }
    public string Type { get; private set; }
    public string Title { get; private set; }
    public string Message { get; private set; }

    private Error(int status, string type, string title, string message)
    {
        Status = status;
        Type = type;
        Title = title;
        Message = message;
    }

    public static Error ConditionNotMet => new(400, "Bad Request", "Condition Not Met", "Condition not met");
    public static Error NullValue => new(400, "Bad Request", "Null Value", "Value cannot be null");
    
    public static Error BadRequest(string title, string message) =>
        new(400, "Bad Request", title, message);

    public static Error Unauthorized(string title, string message) =>
        new(401, "Unauthorized", title, message);

    public static Error NotFound(string title, string message) =>
        new(404, "Not Found", title, message);

    public static Error Conflict(string title, string message) =>
        new(409, "Conflict", title, message);

    public static Error Server(string title, string message) =>
        new(500, "Internal Server Error", title, message);
    
    public static Error Validation(string message) =>
        new(400, "Bad Request", "Validation Error", message);
    public static Error NullOrEmpty(string message) =>
        new(400, "Bad Request", "Null Value Error", message);
}