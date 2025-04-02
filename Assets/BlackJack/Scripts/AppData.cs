using System;

public static class AppData
{
    public static string username;
    public static string email;
    public static string mobile;

    public static string onlineGameCode;
    public static string onlineGameMessage;

    public static string forgotPasswordID;
    public static event Action<GameType> OnUpdateGameType;
    public static GameType gameType;
    public static event Action OnUpdateUserData;

    public static void SetUserData(string userName, string emailId, string mobileNo)
    {
        username = userName;
        email = emailId;
        mobile = mobileNo;
        OnUpdateUserData?.Invoke();
    }

    public static void SetOnlineGameCode(string code,string msg=null)
    {
        onlineGameCode = code;
        onlineGameMessage = msg;
    }
    public static void SetForgotPasswordID(string id)
    {
        forgotPasswordID = id;
    }
   

    public static void SetGameType(GameType type)
    {
        gameType = type;
        OnUpdateGameType?.Invoke(type);
    }

}