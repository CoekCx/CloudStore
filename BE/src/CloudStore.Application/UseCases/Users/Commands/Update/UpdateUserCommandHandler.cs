using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Users;
using CloudStore.Domain.Repositories;
using MediatR;

namespace CloudStore.Application.UseCases.Users.Commands.Update;

public class UpdateUserCommandHandler(
    IUserRepository repository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(new UserId(request.Id), cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }

        user.Update(request.FirstName, request.LastName);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}