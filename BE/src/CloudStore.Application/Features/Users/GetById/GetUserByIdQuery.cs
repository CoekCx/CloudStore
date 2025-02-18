using CloudStore.Application.DTOs.Responses.Users;
using MediatR;

namespace CloudStore.Application.Features.Users.GetById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<UserResponse>;