using CloudStore.Application.Responses.Directories;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Directories;
using MediatR;
using DirectoryNotFoundException = CloudStore.Domain.Exceptions.Directories.DirectoryNotFoundException;

namespace CloudStore.Application.UseCases.Directories.Queries.GetContents;

public class GetDirectoryContentsQueryHandler(IGetDirectoryContentsDataRequest getDirectoryContentsDataRequest)
    : IRequestHandler<GetDirectoryContentsQuery, DirectoryContentsResponse>
{
    public async Task<DirectoryContentsResponse> Handle(GetDirectoryContentsQuery request,
        CancellationToken cancellationToken)
    {
        var directory = await getDirectoryContentsDataRequest.GetAsync(new DirectoryId(request.DirectoryId), cancellationToken);

        if (directory == null)
        {
            throw new DirectoryNotFoundException(request.DirectoryId);
        }

        if (directory.OwnerId != request.OwnerId)
        {
            throw new UnauthorizedDirectoryAccessException();
        }

        return directory;
    }
}