using CloudStore.Application.Abstractions;

namespace CloudStore.Infrastructure.Storage;

public class FileUrlFormatter(StorageBucketOptions storageBucketOptions) : IFileFormatter
{
    public Uri Format(Guid fileId)
    {
        return new
            Uri($"https://{storageBucketOptions.BucketName}.{storageBucketOptions.AwsS3StorageUrl}/files/{fileId}");
    }
}