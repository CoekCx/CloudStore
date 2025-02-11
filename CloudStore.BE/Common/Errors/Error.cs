namespace Common.Errors;

public sealed record Error(int Code, string Description)
{
    #region Common

    public static Error Default => new(0, "Default error");

    public static Error BadRequest => new(1, "Bad request");

    public static Error NotFound => new(2, "Not found");

    public static Error UnprocessableEntity => new(3, "Unprocessable entity");

    public static Error Conflict => new(4, "Conflict");

    public static Error ValidationError => new(5, "Validation error");

    public static Error Unauthorized => new(6, "Unauthorized");

    public static Error Forbidden => new(6, "Forbidden");

    public static Error InternalServerError => new(7, "Internal server error");

    public static Error NotImplemented => new(8, "Not implemented");

    #endregion

    #region Users

    public static Error UserNotFound => new(100, "User not found");

    public static Error UsernameAlreadyExists => new(101, "Username already exists");

    public static Error UserEmailAlreadyExists => new(102, "User email already exists");

    public static Error UserPasswordInvalid => new(103, "User password invalid");

    public static Error UserEmailNotVerified => new(104, "User email not verified");

    #endregion
}