using Common.Abstractions.Contracts.Users;
using MediatR;

namespace UserAdministration.Business.Users.GetById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<IUserResponse>;