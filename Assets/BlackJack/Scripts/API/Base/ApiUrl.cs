public class ApiUrl
{
    private const string BaseURL = "https://potblackjack.com/";
    private const string User = BaseURL + "api/users/";
    public const string Game = BaseURL + "api/game/";
    public const string Login = User + "login";
    public const string SignUp = User + "signup";
    public const string DeleteProfile = User + "delete-account";
    public const string Profile = User + "profile";
    public const string ForgotPassword = User + "forgot-password";
    public const string ResetPassword = User + "reset-password";
    public const string Otp = User + "otp";
}