using CloudStore.Application.Responses.Users;
using MediatR;

namespace CloudStore.Application.UseCases.Auth.GetMe;

public sealed record GetMeQuery(Guid UserId) : IRequest<UserResponse>;