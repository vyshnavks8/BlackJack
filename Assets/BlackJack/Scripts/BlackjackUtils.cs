using System.Net.Mail;

public static class BlackjackUtils
{
    public static bool IsValidEmail(string email)
    {
        var valid = true;

        try
        {
            var emailAddress = new MailAddress(email);
        }
        catch
        {
            valid = false;
        }

        return valid;
    }

    public static bool IsValidMobile(string mobile)
    {
        return mobile.Length is >= 10 and <= 13;
    }

    public static bool IsInputEmpty(string input, string message)
    {
        if (!string.IsNullOrEmpty(input)) return false;
        NetworkPopUp.ShowPopUp("Invalid Input", $"{message} is empty");
        return true;
    }

    public static void ShowEmpty(string message)
    {
        NetworkPopUp.ShowPopUp("Invalid Input", $"{message} is empty");
    }

    public static void ShareOnlineGameCode()
    {
        new NativeShare().SetTitle("Join Black jack private game").SetText("Pot BlackJack \n " +
                                                                           AppData.username +
                                                                           " is inviting to play private game \n \n" +
                                                                           "JOIN GAME CODE : " + AppData.onlineGameCode)
            .Share();
    }
}