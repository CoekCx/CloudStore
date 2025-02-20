using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Users;
using CloudStore.Domain.Repositories;
using CloudStore.Domain.Repositories.Users;
using MediatR;

namespace CloudStore.Application.UseCases.Users.Update;

public class UpdateUserCommandHandler(
    IUserWriteRepository writeRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await writeRepository.GetByIdAsync(new UserId(request.Id), cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }

        user.Update(request.FirstName, request.LastName);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}