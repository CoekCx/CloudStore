using CloudStore.Application.DTOs.Responses.Users;
using MediatR;

namespace CloudStore.Application.Features.Users.Create;

public sealed record CreateUserCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName) : IRequest<UserCreatedResponse>;