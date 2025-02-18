using CloudStore.Application.DTOs.Responses.Directories;
using MediatR;

namespace CloudStore.Application.Features.Directories.GetContents;

public sealed record GetDirectoryContentsQuery(Guid DirectoryId, Guid OwnerId) : IRequest<DirectoryContentsResponse>;