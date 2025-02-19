using CloudStore.Application.Responses.Users;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Users;
using CloudStore.Domain.Repositories.Users;
using MediatR;

namespace CloudStore.Application.UseCases.Users.GetById;

public class GetUserByIdQueryHandler(IUserReadRepository readRepository) : IRequestHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await readRepository.GetByIdAsync(new UserId(request.Id), cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }

        return UserResponse.FromUser(user);
    }
}