using MediatR;

namespace CloudStore.Application.Features.Directories.Delete;

public sealed record DeleteDirectoryCommand(Guid DirectoryId, Guid OwnerId) : IRequest<Unit>;