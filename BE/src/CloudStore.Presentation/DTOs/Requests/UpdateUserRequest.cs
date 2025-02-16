namespace CloudStore.Presentation.DTOs.Requests;

public sealed record UpdateUserRequest(
    string FirstName,
    string LastName);