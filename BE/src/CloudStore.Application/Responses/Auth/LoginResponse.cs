namespace CloudStore.Application.Responses.Auth;

public sealed record LoginResponse(
    Guid Id,
    string Email,
    string Token);