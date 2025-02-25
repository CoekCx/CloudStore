using CloudStore.Application.Responses.Users;
using MediatR;

namespace CloudStore.Application.UseCases.Users.Queries.GetById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<UserResponse>;