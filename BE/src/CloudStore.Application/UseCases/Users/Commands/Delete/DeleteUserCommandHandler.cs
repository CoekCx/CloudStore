using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Users;
using CloudStore.Domain.Repositories;
using MediatR;

namespace CloudStore.Application.UseCases.Users.Commands.Delete;

public class DeleteUserCommandHandler(
    IUserRepository repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(new UserId(request.Id), cancellationToken);
        
        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }

        repository.Delete(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}