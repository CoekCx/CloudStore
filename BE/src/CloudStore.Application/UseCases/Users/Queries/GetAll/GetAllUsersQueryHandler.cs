using CloudStore.Application.Responses.Users;
using MediatR;

namespace CloudStore.Application.UseCases.Users.Queries.GetAll;

public class GetAllUsersQueryHandler(IGetAllUsersDataRequest getAllUsersDataRequest) : IRequestHandler<GetAllUsersQuery, List<UserResponse>>
{
    public async Task<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken) =>
        await getAllUsersDataRequest.GetAsync(new Unit(), cancellationToken);
}