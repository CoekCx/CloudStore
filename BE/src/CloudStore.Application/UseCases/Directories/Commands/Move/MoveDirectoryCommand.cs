using MediatR;

namespace CloudStore.Application.UseCases.Directories.Commands.Move;

public record MoveDirectoryCommand(
    Guid DirectoryId,
    Guid OwnerId,
    Guid? NewParentDirectoryId) : IRequest<Unit>;