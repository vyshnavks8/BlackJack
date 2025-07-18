public static class NetworkPopUp
{
    public static void ShowPopUp(string heading,string message)
    {
        
        PopUpController.ShowPopUp(new PopContent(null,CapitalizeAfterSpace(message)),new ButtonContent("Close",PopUpController.ClosePopUp),ButtonType.ButtonB);
    } public static void ShowPopUp(string heading,string message,ButtonContent extraButton)
    {
        PopUpController.ShowPopUp(new PopContent(heading,CapitalizeAfterSpace(message)),extraButton,new ButtonContent("Close",PopUpController.ClosePopUp));
    }

    private static string CapitalizeAfterSpace(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        var chars = input.ToCharArray();

        var capitalizeNext = true;

        for (var i = 0; i < chars.Length; i++)
        {
            if (char.IsWhiteSpace(chars[i]))
            {
                capitalizeNext = true;
            }
            else if (capitalizeNext)
            {
                chars[i] = char.ToUpper(chars[i]);
                capitalizeNext = false;
            }
        }

        return new string(chars);
    }
}