using CloudStore.Application.Abstractions;
using Microsoft.AspNetCore.Http;

namespace CloudStore.Infrastructure.Storage;

public class FileStorageService(IStorageService storageService) : IFileStorageService
{
    public async Task<Guid> UploadAsync(IFormFile file, CancellationToken cancellationToken = default)
    {
        var fileId = Guid.NewGuid();

        await storageService.UploadAsync(file, FormatKey(fileId), cancellationToken);

        return fileId;
    }

    public async Task<Guid> UploadAsync(byte[] bytes, CancellationToken cancellationToken = default)
    {
        var fileId = Guid.NewGuid();

        await using var memoryStream = new MemoryStream(bytes);

        await storageService.UploadAsync(memoryStream, FormatKey(fileId), cancellationToken);

        return fileId;
    }

    public async Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        await storageService.DeleteAsync(FormatKey(fileId), cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        return await storageService.ExistsAsync(FormatKey(fileId), cancellationToken);
    }

    public async Task<byte[]> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        return await storageService.DownloadAsync(FormatKey(fileId), cancellationToken);
    }

    private static string FormatKey(Guid fileId)
    {
        return $"files/{fileId}";
    }
}