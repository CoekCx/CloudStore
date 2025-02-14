using Common.Abstractions.Contracts.Users;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserAdministration.Business.Abstractions;
using UserAdministration.Business.Users.GetById;

namespace UserAdministration.Business.Users.GetAll;

public class GetAllUsersQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetAllUsersQuery, List<IUserResponse>>
{
    public async Task<List<IUserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await context.Users
            .Select(x => x.Adapt<UserResponse>())
            .Cast<IUserResponse>()
            .ToListAsync(cancellationToken);

        return users;
    }
}