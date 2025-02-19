using CloudStore.Application.Responses.Users;
using CloudStore.Domain.Repositories.Users;
using MediatR;

namespace CloudStore.Application.UseCases.Users.GetAll;

public class GetAllUsersQueryHandler(IUserReadRepository readRepository) : IRequestHandler<GetAllUsersQuery, List<UserResponse>>
{
    public async Task<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await readRepository.GetAllAsync(cancellationToken);

        var userResponses = users
            .Select(UserResponse.FromUser)
            .ToList();

        return userResponses;
    }
}