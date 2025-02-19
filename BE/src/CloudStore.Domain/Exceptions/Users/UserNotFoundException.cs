using CloudStore.Domain.Abstractions.NewFolder;
using CloudStore.Domain.Errors;

namespace CloudStore.Domain.Exceptions.Users;

public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(Guid userId)
        : base("User not found", $"User with the Value {userId} was not found.", Error.UserNotFound)
    {
    }

    public UserNotFoundException(string email)
        : base("User not found", $"User with the email {email} was not found.", Error.UserNotFound)
    {
    }
}