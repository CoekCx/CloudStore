using CloudStore.Domain.Abstractions.Exceptions;
using CloudStore.Domain.Errors;

namespace CloudStore.Domain.Exceptions.Directories;

public sealed class DirectoryNotFoundException(Guid id)
    : NotFoundException(
        "Directory not found",
        $"Directory with the Value {id} was not found.",
        Error.DirectoryNotFound);