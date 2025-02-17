using System;

public static class AppData
{
    public static string username;
    public static string email;
    public static string mobile;

    public static string onlineGameCode;

    public static string forgotPasswordID;


    public static event Action OnUpdateUserData;

    public static void SetUserData(string userName, string emailId, string mobileNo)
    {
        username = userName;
        email = emailId;
        mobile = mobileNo;
        OnUpdateUserData?.Invoke();
    }

    public static void SetOnlineGameCode(string code)
    {
        onlineGameCode = code;
    }
    public static void SetForgotPasswordID(string id)
    {
        forgotPasswordID = id;
    }

}