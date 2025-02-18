using CloudStore.Application.DTOs.Responses.Directories;
using CloudStore.Domain.Abstractions.Repositories.Directories;
using CloudStore.Domain.Abstractions.Repositories.Users;
using CloudStore.Domain.Exceptions.Users;
using MediatR;

namespace CloudStore.Application.Features.Directories.GetRoot;

public class GetRootDirectoryQueryHandler(
    IDirectoryReadRepository directoryReadRepository,
    IUserReadRepository userReadRepository)
    : IRequestHandler<GetRootDirectoryQuery, DirectoryResponse>
{
    public async Task<DirectoryResponse> Handle(GetRootDirectoryQuery request, CancellationToken cancellationToken)
    {
        var owner = await userReadRepository.GetByIdAsync(request.OwnerId, cancellationToken)
                    ?? throw new UserNotFoundException(request.OwnerId);

        var directory = await directoryReadRepository.GetRootDirectory(owner.Id, cancellationToken);

        return DirectoryResponse.FromDirectory(directory);
    }
}