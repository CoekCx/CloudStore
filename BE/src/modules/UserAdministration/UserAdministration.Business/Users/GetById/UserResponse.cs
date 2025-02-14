using Common.Abstractions.Contracts.Users;

namespace UserAdministration.Business.Users.GetById;

public sealed class UserResponse : IUserResponse
{
    public UserResponse()
    {
    }

    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}