using Common.Errors;

namespace Common.Exceptions;

public abstract class NotImplementedException(string title, string message, Error? error = null)
    : BaseException(title, message, error ?? Error.NotImplemented)
{
}