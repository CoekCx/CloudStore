using CloudStore.Application.DTOs.Responses.Directories;
using MediatR;

namespace CloudStore.Application.Features.Directories.Update;

public sealed record UpdateDirectoryCommand(Guid Id, Guid OwnerId, string NewName) : IRequest<DirectoryResponse>;