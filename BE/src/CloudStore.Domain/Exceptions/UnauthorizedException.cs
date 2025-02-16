using CloudStore.Domain.Errors;

namespace CloudStore.Domain.Exceptions;

public abstract class UnauthorizedException(string title, string message, Error? error = null)
    : BaseException(title, message, error ?? Error.Unauthorized)
{
}