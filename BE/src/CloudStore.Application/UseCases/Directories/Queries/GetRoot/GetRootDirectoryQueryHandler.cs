using CloudStore.Application.Responses.Directories;
using CloudStore.Application.UseCases.Users.Queries.GetById;
using CloudStore.Domain.EntityIdentifiers;
using CloudStore.Domain.Exceptions.Users;
using CloudStore.Domain.Exceptions.Directories;
using MediatR;

namespace CloudStore.Application.UseCases.Directories.Queries.GetRoot;

public class GetRootDirectoryQueryHandler(
    IGetUserByIdDataRequest getUserByIdDataRequest,
    IGetRootDirectoryDataRequest getRootDirectoryDataRequest)
    : IRequestHandler<GetRootDirectoryQuery, DirectoryContentsResponse>
{
    public async Task<DirectoryContentsResponse> Handle(GetRootDirectoryQuery request, CancellationToken cancellationToken)
    {
        var owner = await getUserByIdDataRequest.GetAsync(new UserId(request.OwnerId), cancellationToken);

        if (owner == null)
        {
            throw new UserNotFoundException(request.OwnerId);
        }

        var directory = await getRootDirectoryDataRequest.GetAsync(new UserId(request.OwnerId), cancellationToken);

        if (directory == null)
        {
            throw new RootDirectoryNotFoundException(request.OwnerId);
        }

        return directory;
    }
}