using MediatR;

namespace CloudStore.Application.UseCases.Users.Create;

public sealed record CreateUserCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName) : IRequest<Guid>;