using Common.Errors;
using Common.Exceptions;

namespace CloudStore.BE.UserAdministration.Domain.Exceptions.Users;

public sealed class UserWithEmailAlreadyExistsException()
    : ConflictException(
        "User already exists",
        "User with this email already exists.",
        Error.UserEmailAlreadyExists)
{
}