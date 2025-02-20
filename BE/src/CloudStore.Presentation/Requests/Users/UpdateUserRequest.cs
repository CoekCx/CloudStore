namespace CloudStore.Presentation.Requests.Users;

public sealed record UpdateUserRequest(
    string FirstName,
    string LastName);