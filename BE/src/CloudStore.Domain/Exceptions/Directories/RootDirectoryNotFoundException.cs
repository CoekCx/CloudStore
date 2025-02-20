using CloudStore.Domain.Abstractions.Exceptions;
using CloudStore.Domain.Errors;

namespace CloudStore.Domain.Exceptions.Directories;

public sealed class RootDirectoryNotFoundException(Guid ownerId)
    : NotFoundException(
        "Root directory not found",
        $"Root directory with the owner id {ownerId} was not found.",
        Error.DirectoryNotFound);