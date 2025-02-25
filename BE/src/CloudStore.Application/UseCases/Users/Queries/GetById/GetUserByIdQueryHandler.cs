using CloudStore.Application.Responses.Users;
using CloudStore.Domain.EntityIdentifiers;
using MediatR;

namespace CloudStore.Application.UseCases.Users.Queries.GetById;

public class GetUserByIdQueryHandler(IGetUserByIdDataRequest getUserByIdDataRequest) : IRequestHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) =>
        await getUserByIdDataRequest.GetAsync(new UserId(request.Id), cancellationToken);
}