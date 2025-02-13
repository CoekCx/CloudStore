using Common.Domain.Errors;

namespace Common.Domain.Exceptions;

public abstract class NotFoundException(string title, string message, Error? error = null)
    : BaseException(title, message, error ?? Error.NotFound)
{
}