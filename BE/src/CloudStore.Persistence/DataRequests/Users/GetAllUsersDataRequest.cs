using CloudStore.Application.Responses.Users;
using CloudStore.Application.UseCases.Users.Queries.GetAll;
using CloudStore.Domain.EntityIdentifiers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CloudStore.Persistence.DataRequests.Users;

public class GetAllUsersDataRequest(ApplicationDbContext context) : IGetAllUsersDataRequest
{
    public Task<List<UserResponse>> GetAsync(Unit request, CancellationToken cancellationToken = default) =>
        context.Users
            .Select(x => UserResponse.FromUser(x!))
            .ToListAsync(cancellationToken);
}