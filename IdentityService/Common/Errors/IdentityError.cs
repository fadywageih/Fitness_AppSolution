using IdentityService.Domain.Enums;

namespace IdentityService.Common.Errors
{
    public static class IdentityErrors
    {
        public static Error EmailAlreadyExists => new(
            AuthErrorCode.AuthEmailExists,
            "Email is already registered");

        public static Error WeakPassword => new(
            AuthErrorCode.AuthWeakPassword,
            "Password must be at least 6 characters with 1 uppercase and 1 number");

        public static Error InvalidCredentials => new(
            AuthErrorCode.AuthInvalidCredentials,
            "Invalid email or password");

        public static Error AccountLocked => new(
            AuthErrorCode.AuthAccountLocked,
            "Account is locked due to too many failed attempts");

        public static Error TokenInvalid => new(
            AuthErrorCode.AuthTokenInvalid,
            "Token is invalid or malformed");

        public static Error TokenExpired => new(
            AuthErrorCode.AuthTokenExpired,
            "Token has expired");

        public static Error InvalidOtp => new(
            AuthErrorCode.AuthInvalidOtp,
            "Invalid OTP code");

        public static Error OtpExpired => new(
            AuthErrorCode.AuthOtpExpired,
            "OTP code has expired");

        public static Error PasswordMismatch => new(
            AuthErrorCode.AuthPasswordMismatch,
            "New password and confirmation do not match");

        public static Error ResetTokenInvalid => new(
            AuthErrorCode.AuthResetTokenInvalid,
            "Reset token is invalid, expired, or already used");

        public static Error RequiredField(string fieldName) => new(
            AuthErrorCode.ValRequiredField,
            $"{fieldName} is required");

        public static Error UserNotFound => new(
            AuthErrorCode.ResUserNotFound,
            "User not found");

        public static Error OtpResendTooSoon => new(
            AuthErrorCode.RateOtpResendToSoon,
            "Please wait 30 seconds before requesting a new OTP");
    }

    public record Error(AuthErrorCode Code, string Message);
}
