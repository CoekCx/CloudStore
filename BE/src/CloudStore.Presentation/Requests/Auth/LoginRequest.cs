namespace CloudStore.Presentation.Requests.Auth;

public sealed record LoginRequest(
    string Email,
    string Password);