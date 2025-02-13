using Common.Domain.Errors;

namespace Common.Domain.Exceptions;

public abstract class ConflictException(string title, string message, Error? error = null)
    : BaseException(title, message, error ?? Error.Conflict)
{
}