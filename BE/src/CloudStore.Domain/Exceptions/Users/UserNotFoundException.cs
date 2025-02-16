using CloudStore.Domain.Errors;

namespace CloudStore.Domain.Exceptions.Users;

public sealed class UserNotFoundException(Guid userId)
    : NotFoundException(
        "User not found",
        $"User with the Id {userId} was not found.",
        Error.UserNotFound)
{
}