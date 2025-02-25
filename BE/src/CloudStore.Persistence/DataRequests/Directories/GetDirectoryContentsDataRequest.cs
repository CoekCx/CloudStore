using CloudStore.Application.Responses.Directories;
using CloudStore.Application.UseCases.Directories.Queries.GetContents;
using CloudStore.Domain.EntityIdentifiers;
using Microsoft.EntityFrameworkCore;

namespace CloudStore.Persistence.DataRequests.Directories;

public class GetDirectoryContentsDataRequest(ApplicationDbContext context) : IGetDirectoryContentsDataRequest
{
    public Task<DirectoryContentsResponse> GetAsync(DirectoryId request, CancellationToken cancellationToken = default) =>
        context.Directories
            .Include(x => x.Subdirectories)
            .Include(x => x.Files)
            .Where(x => x.Id == request)
            .Select(x => DirectoryContentsResponse.FromDirectory(x))
            .FirstOrDefaultAsync(cancellationToken)!;
}