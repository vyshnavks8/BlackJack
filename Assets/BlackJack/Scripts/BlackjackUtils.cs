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
    public static bool IsInputEmpty(string input,string message)
    {
        if (!string.IsNullOrEmpty(input)) return false;
        NetworkPopUp.ShowPopUp("Invalid Input",$"{message} is empty");
        return true;
    }

}