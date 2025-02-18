using MediatR;

namespace CloudStore.Application.UseCases.Users.Delete;

public sealed record DeleteUserCommand(Guid Id) : IRequest<Unit>;