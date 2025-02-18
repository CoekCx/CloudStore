using CloudStore.Application.DTOs.Responses.Directories;
using MediatR;

namespace CloudStore.Application.Features.Directories.Create;

public sealed record CreateDirectoryCommand(
    string Name,
    Guid OwnerId,
    Guid? ParentDirectoryId) : IRequest<DirectoryResponse>;