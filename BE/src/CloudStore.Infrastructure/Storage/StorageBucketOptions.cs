namespace CloudStore.Infrastructure.Storage;

public class StorageBucketOptions
{
    public string AwsS3StorageUrl { get; set; }

    public string BucketName { get; set; }

    public string Region { get; set; }

    public string AccessKey { get; set; }

    public string SecretKey { get; set; }
}