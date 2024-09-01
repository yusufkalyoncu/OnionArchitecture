using System.Text.Json.Serialization;

namespace OnionArchitecture.Domain.Shared;

public class Result
{
    protected Result(bool isSuccess, Error? error = null)
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
    }
    public bool IsSuccess { get; }
    public Error? Error { get; }
    [JsonIgnore]
    public bool IsFailure => !IsSuccess;
    public static Result Success() => new(true);
    public static Result Failure(Error error) => new(false, error);
}

public class Result<T> : Result
{
    private Result(bool isSuccess, Error? error = null, T? data = default) : base(isSuccess, error)
    {
        Data = data;
    }
    public T? Data { get; }
    public static Result<T> Success(T data) => new(true, null, data);
    public new static Result<T> Failure(Error error) => new(false, error);
}