using CloudStore.Domain.EntityIdentifiers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public static class EntityIdValueConverters
{
    public static ValueConverter<DirectoryId, Guid> DirectoryIdConverter => new(
        directoryId => directoryId.Value,
        value => new DirectoryId(value));

    public static ValueConverter<UserId, Guid> UserIdConverter => new(
        userId => userId.Value,
        value => new UserId(value));

    public static ValueConverter<FileId, Guid> FileIdConverter => new(
        fileId => fileId.Value,
        value => new FileId(value));
} 