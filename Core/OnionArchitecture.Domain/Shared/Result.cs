using System.Text.Json.Serialization;

namespace OnionArchitecture.Domain.Shared;

public class Result<T>
{
    protected internal Result(bool isSuccess, Error? error = null, T? data = default)
    {
        if (isSuccess && error != null)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == null)
        {
            throw new InvalidOperationException();
        }
        
        IsSuccess = isSuccess;
        Error = error;
        Data = data;
    }
    public bool IsSuccess { get; }
    public Error? Error { get; }
    public T? Data { get; set; }
    [JsonIgnore]
    public bool IsFailure => !IsSuccess;
    public static Result<T> Success() => new(true);
    public static Result<T> Success(T data) => new(true, null, data);
    public static Result<T> Failure(Error error) => new(false, error);
}