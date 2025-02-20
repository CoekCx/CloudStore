using CloudStore.Application.Responses.Directories;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Directories;
using CloudStore.Domain.Repositories.Directories;
using MediatR;
using Directory = CloudStore.Domain.Entities.Directory;
using DirectoryNotFoundException = CloudStore.Domain.Exceptions.Directories.DirectoryNotFoundException;

namespace CloudStore.Application.UseCases.Directories.GetById;

public class GetDirectoryByIdQueryHandler(IDirectoryReadRepository readRepository)
    : IRequestHandler<GetDirectoryByIdQuery, DirectoryResponse>
{
    public async Task<DirectoryResponse> Handle(GetDirectoryByIdQuery request, CancellationToken cancellationToken)
    {
        var directory = await readRepository.GetByIdAsync(new DirectoryId(request.DirectoryId), cancellationToken)
                        ?? throw new DirectoryNotFoundException(request.DirectoryId);

        if (directory.OwnerId != new UserId(request.OwnerId))
            throw new UnauthorizedDirectoryAccessException();

        return DirectoryResponse.FromDirectory(directory);
    }
}