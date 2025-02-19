using MediatR;

namespace CloudStore.Application.UseCases.Auth.Login;

public sealed record LoginCommand(
    string Email,
    string Password) : IRequest<string>;