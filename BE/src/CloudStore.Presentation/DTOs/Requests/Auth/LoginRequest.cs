namespace CloudStore.Presentation.DTOs.Requests.Auth;

public sealed record LoginRequest(
    string Email,
    string Password);