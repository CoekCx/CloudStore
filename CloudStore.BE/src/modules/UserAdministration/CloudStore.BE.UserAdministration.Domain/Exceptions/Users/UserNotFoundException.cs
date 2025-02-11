using Common.Errors;
using Common.Exceptions;

namespace CloudStore.BE.UserAdministration.Domain.Exceptions.Users;

public sealed class UserNotFoundException(Guid userId)
    : NotFoundException(
        "User not found",
        $"The user with the id {userId} was not found.",
        Error.UserNotFound)
{
}