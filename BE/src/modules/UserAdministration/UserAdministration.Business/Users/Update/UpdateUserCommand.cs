using MediatR;

namespace UserAdministration.Business.Users.Update;

public sealed record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName) : IRequest<Unit>;