namespace InSynq.Common;

public static class Constants
{
    public const string SOLUTION_NAME = "InSynq";

    public const long SYSTEM_USER = 1;

    public const string AUDIT_LOG_TYPE_INSERT = "INS";
    public const string AUDIT_LOG_TYPE_UPDATE = "UPD";
    public const string AUDIT_LOG_TYPE_DELETE = "DEL";

    public const string CLAIM_ID = "ID";
    public const string CLAIM_USERNAME = "USERNAME";
    public const string CLAIM_EMAIL = "EMAIL";
    public const string CLAIM_ROLES = "ROLES";
    public const int TOKEN_EXPIRATION_TIME = 7;

    public const string ERROR_NOT_FOUND = "Not Found!";
    public const string ERROR_INVALID_OPERATION = "Invalid operation!";
    public const string ERROR_UNAUTHORIZED = "Unauthorized!";
    public const string ERROR_INTERNAL_ERROR = "Internal server error!";

    public const string SORTING_ORDER_DESC = "desc";
    public const string SORTING_ORDER_ASC = "asc";

    // DateTime

    public static readonly DateTime MINIMUM_DATETIME = new(1900, 1, 1, 0, 0, 0);

    // File

    public const int FILE_SIZE_10MB = 10 * 1024 * 1024;
    public static readonly string[] FILE_IMAGE_EXTENSIONS = [".jpg", ".jpeg", ".png"];

    public const string CLOUDINARY_STORAGE_NAME = "insynq-social-media-app";
}