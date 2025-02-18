using CloudStore.Domain.Errors;

namespace CloudStore.Domain.Exceptions.Directories;

public sealed class DirectoryNotFoundException(Guid id)
    : NotFoundException(
        "Directory not found",
        $"Directory with the Id {id} was not found.",
        Error.DirectoryNotFound);