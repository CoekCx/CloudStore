using CloudStore.Domain.Abstractions.NewFolder;
using Error = CloudStore.Domain.Errors.Error;

namespace CloudStore.Domain.Exceptions.Directories;

public sealed class InvalidFolderMoveException(string message = "Root directory modification is forbidden.")
    : InvalidOperationException(
        "Invalid directory move",
        message,
        Error.UnauthorizedAccess);