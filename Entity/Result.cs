namespace Entity;
public class Result
{
    public bool Success { get; }
    public string Error { get; private set; }
    public bool IsFail { get => !Success; }

    protected Result(bool success, string error)
    {
        Success = success;
        Error = error;
    }

    public static Result Fail(string message)
    {
        return new Result(false, message);
    }

    public static Result<T> Fail<T>(string message)
    {
        return new Result<T>(default(T), false, message);
    }

    public static Result Ok()
    {
        return new Result(true, string.Empty);
    }

    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(value, true, string.Empty);
    }
}

public class Result<T> : Result
{
    public T Value { get; set; }

    protected internal Result(T value, bool success, string error)
        : base(success, error)
    {
        this.Value = value;
    }
}