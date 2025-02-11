namespace Common.Constants;

public static class StorageConstants
{
    public const long FreePlanStorageLimit = 1024 * 1024 * 1024; // 1GB
    public const long PremiumPlanStorageLimit = 10 * FreePlanStorageLimit; // 10GB
    public const int MaxFileNameLength = 255;
    public const string AllowedFileExtensions = "jpg,jpeg,png,gif,doc,docx,pdf,txt,zip";
} 