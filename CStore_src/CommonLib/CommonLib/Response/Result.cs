namespace CommonLib.Response
{
    public record Response<T>
    (
        int code,
        string message,
        T data
    );
}


