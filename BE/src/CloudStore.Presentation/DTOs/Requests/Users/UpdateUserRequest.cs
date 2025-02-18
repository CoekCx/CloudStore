namespace CloudStore.Presentation.DTOs.Requests.Users;

public sealed record UpdateUserRequest(
    string FirstName,
    string LastName);