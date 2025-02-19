using CloudStore.Domain.Errors;

namespace CloudStore.Domain.Abstractions.NewFolder;

public abstract class NotFoundException(string title, string message, Error? error = null)
    : BaseException(title, message, error ?? Error.NotFound)
{
}