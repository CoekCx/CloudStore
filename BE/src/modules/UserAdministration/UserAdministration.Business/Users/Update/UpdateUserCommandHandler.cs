using MediatR;
using UserAdministration.Business.Abstractions;
using UserAdministration.Domain.Exceptions;

namespace UserAdministration.Business.Users.Update;

public class UpdateUserCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateUserCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(keyValues: [request.Id], cancellationToken: cancellationToken);

        if (user == null) throw new UserNotFoundException(request.Id);

        user.Update(request.FirstName, request.LastName);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}