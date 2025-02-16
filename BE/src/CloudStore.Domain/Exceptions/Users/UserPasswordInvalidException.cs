using CloudStore.Domain.Errors;

namespace CloudStore.Domain.Exceptions.Users;

public sealed class UserPasswordInvalidException()
    : UnauthorizedException(
        "Invalid password",
        "The provided password is incorrect.",
        Error.UserPasswordInvalid);