public static class ApiData
{
    public static string OtpToken { get; private set; }
    public static string ResetPasswordToken { get; private set; }

    public static void SetOtpToken(string token)
    {
        OtpToken = token;
    } public static void SetResetPasswordToken(string token)
    {
        OtpToken = token;
    }
}