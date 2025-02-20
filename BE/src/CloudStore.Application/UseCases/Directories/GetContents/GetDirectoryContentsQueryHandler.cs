using CloudStore.Application.Responses.Directories;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Directories;
using CloudStore.Domain.Repositories.Directories;
using MediatR;
using DirectoryNotFoundException = CloudStore.Domain.Exceptions.Directories.DirectoryNotFoundException;

namespace CloudStore.Application.UseCases.Directories.GetContents;

public class GetDirectoryContentsQueryHandler(IDirectoryReadRepository directoryReadRepository)
    : IRequestHandler<GetDirectoryContentsQuery, DirectoryContentsResponse>
{
    public async Task<DirectoryContentsResponse> Handle(GetDirectoryContentsQuery request,
        CancellationToken cancellationToken)
    {
        var directory = await directoryReadRepository.GetByIdWithContentsAsync(new DirectoryId(request.DirectoryId), cancellationToken)
                        ?? throw new DirectoryNotFoundException(request.DirectoryId);

        if (directory.OwnerId != new UserId(request.OwnerId))
            throw new UnauthorizedDirectoryAccessException();

        return DirectoryContentsResponse.FromDirectory(directory);
    }
}