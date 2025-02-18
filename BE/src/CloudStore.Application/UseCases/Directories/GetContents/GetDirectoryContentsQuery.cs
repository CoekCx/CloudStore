using CloudStore.Application.Responses.Directories;
using MediatR;

namespace CloudStore.Application.UseCases.Directories.GetContents;

public sealed record GetDirectoryContentsQuery(Guid DirectoryId, Guid OwnerId) : IRequest<DirectoryContentsResponse>;