using CloudStore.Application.Responses.Users;
using MediatR;

namespace CloudStore.Application.UseCases.Users.GetAll;

public sealed record GetAllUsersQuery : IRequest<List<UserResponse>>;