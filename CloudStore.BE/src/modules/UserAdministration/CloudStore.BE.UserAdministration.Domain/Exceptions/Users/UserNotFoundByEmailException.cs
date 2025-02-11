using Common.Errors;
using Common.Exceptions;

namespace CloudStore.BE.UserAdministration.Domain.Exceptions.Users;

public sealed class UserNotFoundByEmailException(string email)
    : NotFoundException(
        "User not found",
        $"User with the email {email} not found.",
        Error.UserNotFound)
{
}