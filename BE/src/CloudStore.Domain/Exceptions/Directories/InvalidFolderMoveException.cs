using Error = CloudStore.Domain.Errors.Error;
using Exceptions_InvalidOperationException = CloudStore.Domain.Abstractions.Exceptions.InvalidOperationException;
using InvalidOperationException = CloudStore.Domain.Abstractions.Exceptions.InvalidOperationException;

namespace CloudStore.Domain.Exceptions.Directories;

public sealed class InvalidFolderMoveException(string message = "Root directory modification is forbidden.")
    : Exceptions_InvalidOperationException(
        "Invalid directory move",
        message,
        Error.UnauthorizedAccess);