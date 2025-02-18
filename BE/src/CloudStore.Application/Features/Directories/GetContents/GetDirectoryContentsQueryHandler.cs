using CloudStore.Application.DTOs.Responses.Directories;
using CloudStore.Domain.Abstractions.Repositories.Directories;
using CloudStore.Domain.Exceptions.Directories;
using MediatR;
using DirectoryNotFoundException = CloudStore.Domain.Exceptions.Directories.DirectoryNotFoundException;

namespace CloudStore.Application.Features.Directories.GetContents;

public class GetDirectoryContentsQueryHandler(IDirectoryReadRepository directoryReadRepository)
    : IRequestHandler<GetDirectoryContentsQuery, DirectoryContentsResponse>
{
    public async Task<DirectoryContentsResponse> Handle(GetDirectoryContentsQuery request,
        CancellationToken cancellationToken)
    {
        var directory = await directoryReadRepository.GetByIdWithContentsAsync(request.DirectoryId, cancellationToken)
                        ?? throw new DirectoryNotFoundException(request.DirectoryId);

        if (directory.OwnerId != request.OwnerId)
            throw new UnauthorizedDirectoryAccessException();

        return DirectoryContentsResponse.FromDirectory(directory);
    }
}