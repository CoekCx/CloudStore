using MediatR;

namespace CloudStore.Application.UseCases.Directories.Move;

public record MoveDirectoryCommand(
    Guid DirectoryId,
    Guid OwnerId,
    Guid? NewParentDirectoryId) : IRequest<Unit>;