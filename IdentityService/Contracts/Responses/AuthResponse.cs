namespace IdentityService.Contracts.Responses
{
    public record AuthResponse(
        bool IsSuccess,
        string Message,
        object? Data,
        string[]? Errors,
        int StatusCode,
        DateTime Timestamp
    )
    {
        public static AuthResponse Success(object? data = null, string message = "Success")
            => new(true, message, data, null, 200, DateTime.UtcNow);

        public static AuthResponse Created(object? data = null, string message = "Created")
            => new(true, message, data, null, 201, DateTime.UtcNow);

        public static AuthResponse Failure(string message, int statusCode, string[]? errors = null)
            => new(false, message, null, errors, statusCode, DateTime.UtcNow);
    }
}
