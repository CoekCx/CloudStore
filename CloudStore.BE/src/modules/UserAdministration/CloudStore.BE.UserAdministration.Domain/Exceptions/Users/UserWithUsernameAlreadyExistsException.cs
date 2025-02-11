using Common.Errors;
using Common.Exceptions;

namespace CloudStore.BE.UserAdministration.Domain.Exceptions.Users;

public sealed class UserWithUsernameAlreadyExistsException()
    : ConflictException(
        "User already exists",
        "User with this username already exists.",
        Error.UsernameAlreadyExists)
{
}