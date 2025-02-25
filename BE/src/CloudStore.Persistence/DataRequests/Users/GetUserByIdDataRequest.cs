using CloudStore.Application.Responses.Users;
using CloudStore.Application.UseCases.Users.Queries.GetById;
using CloudStore.Domain.EntityIdentifiers;
using Microsoft.EntityFrameworkCore;

namespace CloudStore.Persistence.DataRequests.Users;

public class GetUserByIdDataRequest(ApplicationDbContext context) : IGetUserByIdDataRequest
{
    public Task<UserResponse> GetAsync(UserId request, CancellationToken cancellationToken = default) =>
        context.Users
            .Where(x => x!.Id == request)
            .Select(x => UserResponse.FromUser(x!))
            .FirstOrDefaultAsync(cancellationToken)!;
}