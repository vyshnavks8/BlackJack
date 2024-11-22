using System;

public static class AppData 
{
    public static string username;
    public static string email;
    public static string mobile;
    public static event Action OnUpdateUserData;
    public static void SetUserData(string userName,string emailId, string mobileNo)
    {
        username = userName;
        email = emailId;
        mobile = mobileNo;
        OnUpdateUserData?.Invoke();
    }
}