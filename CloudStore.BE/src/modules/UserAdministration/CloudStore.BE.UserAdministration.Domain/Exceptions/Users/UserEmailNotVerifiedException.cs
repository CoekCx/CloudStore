using Common.Errors;
using Common.Exceptions;

namespace CloudStore.BE.UserAdministration.Domain.Exceptions.Users;

public sealed class UserEmailNotVerifiedException()
    : UnauthorizedException(
        "User email is not verified",
        "The user needs to verify their email address to continue.",
        Error.UserEmailNotVerified)
{
}