using MediatR;

namespace CloudStore.Application.Features.Directories.Move;

public record MoveDirectoryCommand(
    Guid DirectoryId,
    Guid OwnerId,
    Guid? NewParentDirectoryId) : IRequest<Unit>;