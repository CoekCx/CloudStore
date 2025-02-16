using CloudStore.Application.DTOs.Responses;
using CloudStore.Domain.Abstractions.Repositories.Users;
using CloudStore.Domain.Exceptions.Users;
using MediatR;

namespace CloudStore.Application.Features.Auth.GetMe;

public sealed class GetMeQueryHandler(IUserReadRepository userReadRepository) 
    : IRequestHandler<GetMeQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var user = await userReadRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            throw new UserNotFoundException(request.UserId);

        return UserResponse.FromUser(user);
    }
} 