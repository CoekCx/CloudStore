using CloudStore.Application.Responses.Directories;
using MediatR;

namespace CloudStore.Application.UseCases.Directories.Commands.Create;

public sealed record CreateDirectoryCommand(
    string Name,
    Guid OwnerId,
    Guid? ParentDirectoryId) : IRequest<DirectoryResponse>;