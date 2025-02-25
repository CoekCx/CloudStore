using CloudStore.Application.Responses.Directories;
using MediatR;

namespace CloudStore.Application.UseCases.Directories.Queries.GetRoot;

public sealed record GetRootDirectoryQuery(Guid OwnerId) : IRequest<DirectoryContentsResponse>;