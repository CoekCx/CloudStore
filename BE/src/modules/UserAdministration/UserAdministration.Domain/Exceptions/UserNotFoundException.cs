using Common.Domain.Errors;
using Common.Domain.Exceptions;

namespace UserAdministration.Domain.Exceptions;

public sealed class UserNotFoundException(Guid userId)
    : NotFoundException(
        "User not found",
        $"User with the Id {userId} was not found.",
        Error.UserNotFound)
{
}