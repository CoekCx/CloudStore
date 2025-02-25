using MediatR;

namespace CloudStore.Application.UseCases.Files.Commands.Move;

public record MoveFileCommand(
    Guid FileId,
    Guid OwnerId,
    Guid? NewParentDirectoryId) : IRequest<Unit>;