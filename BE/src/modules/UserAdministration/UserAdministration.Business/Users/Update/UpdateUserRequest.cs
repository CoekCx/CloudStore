namespace UserAdministration.Business.Users.Update;

public sealed record UpdateUserRequest(
    string FirstName,
    string LastName);