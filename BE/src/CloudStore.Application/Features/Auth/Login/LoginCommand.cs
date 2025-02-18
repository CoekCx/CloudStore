using CloudStore.Application.DTOs.Responses.Auth;
using MediatR;

namespace CloudStore.Application.Features.Auth.Login;

public sealed record LoginCommand(
    string Email,
    string Password) : IRequest<LoginResponse>;