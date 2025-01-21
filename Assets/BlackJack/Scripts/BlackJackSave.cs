using UnityEngine;

public static class BlackJackSave
{
    private const string LOGIN_TOKEN = "login_token";

    public static void SetLoginToken(string token)
    {
        PlayerPrefs.SetString(LOGIN_TOKEN, token);
    }

    public static string GetLogin()
    {
        return PlayerPrefs.GetString(LOGIN_TOKEN);
    }
}