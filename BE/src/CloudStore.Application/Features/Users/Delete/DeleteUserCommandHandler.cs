using CloudStore.Domain.Abstractions.Repositories.Users;
using CloudStore.Domain.Exceptions.Users;
using MediatR;

namespace CloudStore.Application.Features.Users.Delete;

public class DeleteUserCommandHandler(IUserWriteRepository writeRepository, IUserReadRepository readRepository)
    : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await readRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user == null) throw new UserNotFoundException(request.Id);

        await writeRepository.DeleteAsync(user, cancellationToken);

        return Unit.Value;
    }
}