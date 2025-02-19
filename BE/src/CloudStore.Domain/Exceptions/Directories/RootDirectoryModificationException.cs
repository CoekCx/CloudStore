using CloudStore.Domain.Abstractions.NewFolder;
using Error = CloudStore.Domain.Errors.Error;

namespace CloudStore.Domain.Exceptions.Directories;

public sealed class RootDirectoryModificationException()
    : UnauthorizedException(
        "Unauthorized",
        "Root directory modification is forbidden.",
        Error.UnauthorizedAccess);