namespace Restaurants_Platform.HelperClasses;

public class Result<T>
{
    public T? Data { get; }
    public bool Success { get; }
    public string Message { get; }

    private Result(T? data, bool success, string message)
    {
        Data = data;
        Success = success;
        Message = message;
    }

    public static Result<T> SuccessResult(T data, string message = "")
    {
        return new Result<T>(data, true, message);
    }

    public static Result<T> FailureResult(string message)
    {
        return new Result<T>(default, false, message);
    }
}
