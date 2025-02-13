using Common.Domain.Errors;
using Common.Domain.Exceptions;

namespace UserAdministration.Domain.Exceptions;

public class UserEmailAlreadyExists(string email)
    : ConflictException(
        "Email already exists",
        $"A user with the email {email} already exists.",
        Error.UserEmailAlreadyExists)
{
}