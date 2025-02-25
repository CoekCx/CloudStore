using MediatR;

namespace CloudStore.Application.UseCases.Users.Commands.Update;

public sealed record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName) : IRequest<Unit>;