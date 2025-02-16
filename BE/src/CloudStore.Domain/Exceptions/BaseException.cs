using CloudStore.Domain.Errors;

namespace CloudStore.Domain.Exceptions;

public abstract class BaseException(string title, string message, Error? error = null) : Exception(message)
{
    public string Title { get; set; } = title;
    public Error Error { get; set; } = error ?? Error.Default;
}