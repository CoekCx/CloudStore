using CloudStore.Domain.Errors;

namespace CloudStore.Domain.Abstractions.Exceptions;

public class UnauthorizedException(
    string title = "Unauthorized",
    string message = "User is not authenticated.",
    Error? error = null)
    : BaseException(title, message, error ?? Error.Unauthorized)
{
}