using Common.Abstractions.Contracts.Users;
using MediatR;

namespace UserAdministration.Business.Users.GetAll;

public sealed record GetAllUsersQuery : IRequest<List<IUserResponse>>;