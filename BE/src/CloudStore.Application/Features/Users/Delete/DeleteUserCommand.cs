using MediatR;

namespace CloudStore.Application.Features.Users.Delete;

public sealed record DeleteUserCommand(Guid Id) : IRequest<Unit>;