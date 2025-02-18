using CloudStore.Application.DTOs.Responses.Directories;
using MediatR;

namespace CloudStore.Application.Features.Directories.GetById;

public sealed record GetDirectoryByIdQuery(Guid DirectoryId, Guid OwnerId) : IRequest<DirectoryResponse>;