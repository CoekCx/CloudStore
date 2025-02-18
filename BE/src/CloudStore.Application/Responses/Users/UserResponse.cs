using CloudStore.Domain.Entities;

namespace CloudStore.Application.Responses.Users;

public sealed record UserResponse(Guid Id, string Email, string FirstName, string LastName)
{
    public static UserResponse FromUser(User user)
    {
        return new UserResponse(user.Id, user.Email, user.FirstName, user.LastName);
    }
}