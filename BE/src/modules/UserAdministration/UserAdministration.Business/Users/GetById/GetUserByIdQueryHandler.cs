using Common.Abstractions.Contracts.Users;
using Mapster;
using MediatR;
using UserAdministration.Business.Abstractions;
using UserAdministration.Domain.Entities;
using UserAdministration.Domain.Exceptions;

namespace UserAdministration.Business.Users.GetById;

public class GetUserByIdQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetUserByIdQuery, IUserResponse>
{
    public async Task<IUserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(keyValues: [request.Id], cancellationToken: cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }

        return user.Adapt<UserResponse>();
    }
}