using CloudStore.Application.Responses.Directories;
using MediatR;

namespace CloudStore.Application.UseCases.Directories.Update;

public sealed record UpdateDirectoryCommand(Guid Id, Guid OwnerId, string NewName) : IRequest<DirectoryResponse>;