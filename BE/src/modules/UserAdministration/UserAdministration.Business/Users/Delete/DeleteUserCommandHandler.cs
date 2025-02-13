using MediatR;
using UserAdministration.Business.Abstractions;
using UserAdministration.Domain.Exceptions;

namespace UserAdministration.Business.Users.Delete;

public class DeleteUserCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(keyValues: [request.UserId], cancellationToken: cancellationToken);

        if (user == null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}