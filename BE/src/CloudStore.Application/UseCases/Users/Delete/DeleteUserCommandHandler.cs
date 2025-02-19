using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Users;
using CloudStore.Domain.Repositories;
using CloudStore.Domain.Repositories.Users;
using MediatR;

namespace CloudStore.Application.UseCases.Users.Delete;

public class DeleteUserCommandHandler(
    IUserWriteRepository writeRepository,
    IUserReadRepository readRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await readRepository.GetByIdAsync(new UserId(request.Id), cancellationToken);
        
        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }

        writeRepository.Remove(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}