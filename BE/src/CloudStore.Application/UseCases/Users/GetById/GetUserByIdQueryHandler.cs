using CloudStore.Application.Responses.Users;
using CloudStore.Domain.Abstractions.Repositories.Users;
using CloudStore.Domain.Exceptions.Users;
using MediatR;

namespace CloudStore.Application.UseCases.Users.GetById;

public class GetUserByIdQueryHandler(IUserReadRepository readRepository)
    : IRequestHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await readRepository.GetByIdAsync(request.Id, cancellationToken)
                   ?? throw new UserNotFoundException(request.Id);

        return UserResponse.FromUser(user);
    }
}