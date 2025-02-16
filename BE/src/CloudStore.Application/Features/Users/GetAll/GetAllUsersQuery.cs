using CloudStore.Application.DTOs.Responses;
using MediatR;

namespace CloudStore.Application.Features.Users.GetAll;

public sealed record GetAllUsersQuery : IRequest<List<UserResponse>>;