using CloudStore.Domain.Errors;

namespace CloudStore.Domain.Abstractions.NewFolder;

public abstract class UnProcessableEntityException(string title, string message, Error? error = null)
    : BaseException(title, message, error ?? Error.UnprocessableEntity)
{
}