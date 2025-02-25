using CloudStore.Application.Responses.Directories;
using CloudStore.Application.UseCases.Directories.Queries.GetRoot;
using CloudStore.Domain.EntityIdentifiers;
using Microsoft.EntityFrameworkCore;

namespace CloudStore.Persistence.DataRequests.Directories;

public class GetRootDirectoryDataRequest(ApplicationDbContext context) : IGetRootDirectoryDataRequest
{
    public Task<DirectoryContentsResponse> GetAsync(UserId request, CancellationToken cancellationToken = default) =>
        context.Directories
            .Include(x => x.Subdirectories)
            .Include(x => x.Files)
            .Where(x => x.OwnerId == request && x.ParentDirectoryId == null)
            .Select(x => DirectoryContentsResponse.FromDirectory(x))
            .FirstOrDefaultAsync(cancellationToken)!;
}