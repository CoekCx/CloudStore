namespace Common.Contracts.Authentication;

public sealed record GenerateTokenRequest(string Email, Guid Id);