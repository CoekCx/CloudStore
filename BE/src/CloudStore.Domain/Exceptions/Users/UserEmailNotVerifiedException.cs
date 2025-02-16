using CloudStore.Domain.Errors;

namespace CloudStore.Domain.Exceptions.Users;

public sealed class UserEmailNotVerifiedException()
    : UnauthorizedException(
        "Email not verified",
        "The user's email address has not been verified.",
        Error.UserEmailNotVerified);