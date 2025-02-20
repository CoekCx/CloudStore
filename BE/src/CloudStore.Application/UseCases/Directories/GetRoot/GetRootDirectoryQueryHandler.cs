using CloudStore.Application.Responses.Directories;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Users;
using CloudStore.Domain.Repositories.Directories;
using CloudStore.Domain.Repositories.Users;
using MediatR;
using Directory = CloudStore.Domain.Entities.Directory;

namespace CloudStore.Application.UseCases.Directories.GetRoot;

public class GetRootDirectoryQueryHandler(
    IDirectoryReadRepository directoryReadRepository,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetRootDirectoryQuery, DirectoryResponse>
{
    public async Task<DirectoryResponse> Handle(GetRootDirectoryQuery request, CancellationToken cancellationToken)
    {
        var owner = await userReadRepository.GetByIdAsync(new UserId(request.OwnerId), cancellationToken)
                    ?? throw new UserNotFoundException(request.OwnerId);

        var directory = await directoryReadRepository.GetRootDirectory(owner.Id, cancellationToken);

        return DirectoryResponse.FromDirectory(directory);
    }
}