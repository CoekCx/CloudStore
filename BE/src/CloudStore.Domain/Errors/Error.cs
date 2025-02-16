namespace CloudStore.Domain.Errors;

public sealed record Error(int Code, string Description)
{
    #region Common

    public static Error Default => new(0, "Default error");

    public static Error BadRequest => new(1, "Bad request");

    public static Error NotFound => new(2, "Not found");

    public static Error UnprocessableEntity => new(3, "Unprocessable entity");

    public static Error Conflict => new(4, "Conflict");

    #endregion

    #region Users

    public static Error UserNotFound => new(100, "User not found");

    public static Error UserPasswordInvalid => new(103, "User password invalid");

    public static Error UserEmailNotVerified => new(104, "User email not verified");

    public static Error UserEmailAlreadyExists => new(105, "User with that email already exists");

    #endregion
}