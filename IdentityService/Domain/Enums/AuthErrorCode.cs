namespace IdentityService.Domain.Enums
{
    public enum AuthErrorCode
    {
        // Success
        Success = 0,

        // General
        Unknown = 1000,

        // Authentication
        AuthEmailExists = 2000,
        AuthWeakPassword = 2001,
        AuthInvalidCredentials = 2002,
        AuthAccountLocked = 2003,
        AuthTokenInvalid = 2004,
        AuthTokenExpired = 2005,
        AuthInvalidOtp = 2006,
        AuthOtpExpired = 2007,
        AuthPasswordMismatch = 2008,
        AuthResetTokenInvalid = 2009,

        // Validation
        ValRequiredField = 3000,
        ValInvalidAge = 3001,
        ValInvalidWeight = 3002,
        ValInvalidHeight = 3003,
        ValInvalidGender = 3004,
        ValInvalidGoal = 3005,
        ValInvalidActivity = 3006,
        ValInvalidDate = 3007,
        ValInvalidFileType = 3008,
        ValFileTooLarge = 3009,

        // Resource
        ResNotFound = 4000,
        ResWorkoutNotFound = 4001,
        ResExerciseNotFound = 4002,
        ResPlanNotFound = 4003,
        ResSessionNotFound = 4004,
        ResMealNotFound = 4005,
        ResUserNotFound = 4006,

        // Fitness Engine
        FceStatsNotFound = 5000,
        FceMetricsNotCalculated = 5001,
        FceNoMatchingPlan = 5002,
        FceInvalidCalculation = 5003,

        // Service
        SrvFileUploadFailed = 6000,
        SrvServiceUnavailable = 6001,
        SrvDatabaseError = 6002,

        // Permission
        PermPremiumRequired = 7000,

        // Rate Limiting
        RateLimitExceeded = 8000,
        RateOtpResendToSoon = 8001
    }
}
