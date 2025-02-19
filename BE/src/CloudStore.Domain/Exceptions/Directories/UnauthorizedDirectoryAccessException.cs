using CloudStore.Domain.Abstractions.NewFolder;
using CloudStore.Domain.Errors;

namespace CloudStore.Domain.Exceptions.Directories;

public sealed class UnauthorizedDirectoryAccessException()
    : UnauthorizedException(
        "Unauthorized",
        "User is not the owner of this directory.",
        Error.UnauthorizedAccess);