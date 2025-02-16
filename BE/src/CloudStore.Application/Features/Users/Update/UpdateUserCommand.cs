using MediatR;

namespace CloudStore.Application.Features.Users.Update;

public sealed record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName) : IRequest<Unit>;