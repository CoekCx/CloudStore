using CloudStore.Application.Responses.Directories;
using MediatR;

namespace CloudStore.Application.UseCases.Directories.Queries.GetById;

public sealed record GetDirectoryByIdQuery(Guid DirectoryId, Guid OwnerId) : IRequest<DirectoryResponse>;