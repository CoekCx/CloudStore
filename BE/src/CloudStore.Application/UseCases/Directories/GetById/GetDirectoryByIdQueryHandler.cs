using CloudStore.Application.Responses.Directories;
using CloudStore.Domain.Abstractions.Repositories.Directories;
using CloudStore.Domain.Exceptions.Directories;
using MediatR;
using DirectoryNotFoundException = CloudStore.Domain.Exceptions.Directories.DirectoryNotFoundException;

namespace CloudStore.Application.UseCases.Directories.GetById;

public class GetDirectoryByIdQueryHandler(IDirectoryReadRepository readRepository)
    : IRequestHandler<GetDirectoryByIdQuery, DirectoryResponse>
{
    public async Task<DirectoryResponse> Handle(GetDirectoryByIdQuery request, CancellationToken cancellationToken)
    {
        var directory = await readRepository.GetByIdAsync(request.DirectoryId, cancellationToken)
                        ?? throw new DirectoryNotFoundException(request.DirectoryId);

        if (directory.OwnerId != request.OwnerId)
            throw new UnauthorizedDirectoryAccessException();

        return DirectoryResponse.FromDirectory(directory);
    }
}