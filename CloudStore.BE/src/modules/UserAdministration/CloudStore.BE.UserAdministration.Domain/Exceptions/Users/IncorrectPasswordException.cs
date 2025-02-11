using Common.Errors;
using Common.Exceptions;

namespace CloudStore.BE.UserAdministration.Domain.Exceptions.Users;

public sealed class IncorrectPasswordException()
    : BadRequestException(
        "Incorrect password",
        "Password is incorrect.",
        Error.UserPasswordInvalid)
{
}