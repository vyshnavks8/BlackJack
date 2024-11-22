public class ResetPasswordData
{
    public string token;
    public string newPassword;
}
public class ResetPasswordResponse : BaseResponse
{
    public string message;
}