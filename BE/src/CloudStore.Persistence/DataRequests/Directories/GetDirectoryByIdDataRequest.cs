using CloudStore.Application.Responses.Directories;
using CloudStore.Application.UseCases.Directories.Queries.GetById;
using CloudStore.Domain.EntityIdentifiers;
using Microsoft.EntityFrameworkCore;

namespace CloudStore.Persistence.DataRequests.Directories;

public class GetDirectoryByIdDataRequest(ApplicationDbContext context) : IGetDirectoryByIdDataRequest
{
    public Task<DirectoryResponse> GetAsync(DirectoryId request, CancellationToken cancellationToken = default) =>
        context.Directories
            .Where(x => x.Id == request)
            .Select(x => DirectoryResponse.FromDirectory(x))
            .FirstOrDefaultAsync(cancellationToken)!;
}