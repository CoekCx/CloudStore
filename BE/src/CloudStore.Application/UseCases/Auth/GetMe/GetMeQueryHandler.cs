using CloudStore.Application.Responses.Users;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Users;
using CloudStore.Domain.Repositories.Users;
using MediatR;

namespace CloudStore.Application.UseCases.Auth.GetMe;

public sealed class GetMeQueryHandler(IUserReadRepository userReadRepository) : IRequestHandler<GetMeQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var user = await userReadRepository.GetByIdAsync(new UserId(request.UserId), cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        return UserResponse.FromUser(user);
    }
}