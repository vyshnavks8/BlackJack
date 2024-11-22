public class ForgotPasswordData
{
    public string email;
}
public class ForgotPasswordResponse : BaseResponse
{
    public string message;
    public int otp;
}

