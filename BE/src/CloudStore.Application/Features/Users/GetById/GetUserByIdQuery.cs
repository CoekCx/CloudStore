using CloudStore.Application.DTOs.Responses;
using MediatR;

namespace CloudStore.Application.Features.Users.GetById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<UserResponse>;