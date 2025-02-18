using CloudStore.Domain.Abstractions;
using CloudStore.Domain.Abstractions.Repositories.Users;
using CloudStore.Domain.Exceptions.Users;
using MediatR;

namespace CloudStore.Application.Features.Users.Delete;

public class DeleteUserCommandHandler(
    IUserWriteRepository writeRepository,
    IUserReadRepository readRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await readRepository.GetByIdAsync(request.Id, cancellationToken)
                   ?? throw new UserNotFoundException(request.Id);

        await writeRepository.DeleteAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}