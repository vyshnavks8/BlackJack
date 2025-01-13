public static class NetworkPopUp
{
    public static void ShowPopUp(string heading,string message)
    {
        PopUpController.ShowPopUp(new PopContent(null,message),new ButtonContent("Close",PopUpController.ClosePopUp),ButtonType.ButtonB);
    } public static void ShowPopUp(string heading,string message,ButtonContent extraButton)
    {
        PopUpController.ShowPopUp(new PopContent(heading,message),extraButton,new ButtonContent("Close",PopUpController.ClosePopUp));
    }
    
}