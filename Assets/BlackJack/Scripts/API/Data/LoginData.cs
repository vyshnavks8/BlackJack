public class LoginData
{
    public string email;
    public string password;
}
public class LoginResponse : BaseResponse
{
    public string message { get; set; }
}