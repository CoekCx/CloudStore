namespace CloudStore.Presentation.DTOs.Requests.Users;

public sealed record CreateUserRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName);