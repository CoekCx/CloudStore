namespace UserAdministration.Business.Users.Create;

public sealed record CreateUserRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName);