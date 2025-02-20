namespace CloudStore.Presentation.Requests.Users;

public sealed record CreateUserRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName);