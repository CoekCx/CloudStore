using MediatR;

namespace UserAdministration.Business.Users.Delete;

public sealed record DeleteUserCommand(Guid UserId) : IRequest<bool>;