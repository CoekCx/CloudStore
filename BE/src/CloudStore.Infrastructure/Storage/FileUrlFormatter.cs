using CloudStore.Application.Abstractions;
using Microsoft.Extensions.Options;

namespace CloudStore.Infrastructure.Storage;

public class FileUrlFormatter(IOptions<StorageBucketOptions> storageBucketOptions) : IFileFormatter
{
    public Uri Format(Guid fileId)
    {
        return new
            Uri($"https://{storageBucketOptions.Value.BucketName}.{storageBucketOptions.Value.AwsS3StorageUrl}/files/{fileId}");
    }
}