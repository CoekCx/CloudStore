using MediatR;

namespace CloudStore.Application.UseCases.Directories.Delete;

public sealed record DeleteDirectoryCommand(Guid DirectoryId, Guid OwnerId) : IRequest<Unit>;