public static class ApiData
{
    public static string ForgotPasswordToken { get; private set; }

    public static void SetForgotPasswordToken(string token)
    {
        ForgotPasswordToken = token;
    } 
   
}