using MediatR;

namespace CloudStore.Application.UseCases.Users.Commands.Delete;

public sealed record DeleteUserCommand(Guid Id) : IRequest<Unit>;