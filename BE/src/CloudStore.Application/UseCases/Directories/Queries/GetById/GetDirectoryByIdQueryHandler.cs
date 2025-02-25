using CloudStore.Application.Responses.Directories;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Directories;
using MediatR;
using DirectoryNotFoundException = CloudStore.Domain.Exceptions.Directories.DirectoryNotFoundException;

namespace CloudStore.Application.UseCases.Directories.Queries.GetById;

public class GetDirectoryByIdQueryHandler(IGetDirectoryByIdDataRequest getDirectoryByIdDataRequest)
    : IRequestHandler<GetDirectoryByIdQuery, DirectoryResponse>
{
    public async Task<DirectoryResponse> Handle(GetDirectoryByIdQuery request, CancellationToken cancellationToken)
    {
        var directory = await getDirectoryByIdDataRequest.GetAsync(new DirectoryId(request.DirectoryId), cancellationToken);

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