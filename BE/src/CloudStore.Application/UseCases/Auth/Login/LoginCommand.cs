using CloudStore.Application.Responses.Auth;
using MediatR;

namespace CloudStore.Application.UseCases.Auth.Login;

public sealed record LoginCommand(
    string Email,
    string Password) : IRequest<LoginResponse>;