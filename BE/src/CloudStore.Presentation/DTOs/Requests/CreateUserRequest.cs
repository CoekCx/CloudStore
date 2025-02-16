namespace CloudStore.Presentation.DTOs.Requests;

public sealed record CreateUserRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName);