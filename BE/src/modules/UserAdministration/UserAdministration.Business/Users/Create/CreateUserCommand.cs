using Common.Abstractions.Contracts;
using MediatR;

namespace UserAdministration.Business.Users.Create;

public sealed record CreateUserCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName) : IRequest<EntityCreatedResponse>;