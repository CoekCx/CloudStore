using CloudStore.Domain.Abstractions.NewFolder;
using Error = CloudStore.Domain.Errors.Error;

namespace CloudStore.Domain.Exceptions.Directories;

public sealed class RootDirectoryCreationException()
    : UnauthorizedException(
        "Unauthorized",
        "Root directory creation is forbidden.",
        Error.UnauthorizedAccess);