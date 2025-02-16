using CloudStore.Application.DTOs.Responses;
using MediatR;

namespace CloudStore.Application.Features.Auth.GetMe;

public sealed record GetMeQuery(Guid UserId) : IRequest<UserResponse>; 