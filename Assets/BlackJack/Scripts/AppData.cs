using System;
using UnityEngine;

public static class AppData
{
    public static string username;
    public static string email;
    public static string mobile;
    public static string iconUrl;
    public static Sprite profileIcon;

    public static string onlineGameCode;
    public static string onlineGameMessage;

    public static string forgotPasswordID;
    public static event Action<GameType> OnUpdateGameType;
    public static GameType gameType;
    public static event Action OnUpdateUserData;

    public static void SetUserData(string userName, string emailId, string mobileNo, string icon)
    {
        username = userName;
        email = emailId;
        mobile = mobileNo;
        iconUrl = icon;
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

    public static void SetUserIcon(Sprite texture)
    {
        profileIcon=texture;
    }

    public static void ClearData()
    {
        username = null;
        email = null;
        mobile = null;
        iconUrl = null;
        profileIcon = null;
        onlineGameCode = null;
        onlineGameMessage = null;
        forgotPasswordID = null;
        
    }
}