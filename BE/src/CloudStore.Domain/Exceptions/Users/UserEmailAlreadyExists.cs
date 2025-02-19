using CloudStore.Domain.Abstractions.NewFolder;
using CloudStore.Domain.Errors;

namespace CloudStore.Domain.Exceptions.Users;

public class UserEmailAlreadyExists(string email)
    : ConflictException(
        "Email already exists",
        $"A user with the email {email} already exists.",
        Error.UserEmailAlreadyExists)
{
}