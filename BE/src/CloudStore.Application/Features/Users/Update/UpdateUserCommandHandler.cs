using CloudStore.Domain.Abstractions;
using CloudStore.Domain.Abstractions.Repositories.Users;
using MediatR;

namespace CloudStore.Application.Features.Users.Update;

public class UpdateUserCommandHandler(IUserWriteRepository writeRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateUserCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await writeRepository.UpdateAsync(request.Id, request.FirstName, request.LastName, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}