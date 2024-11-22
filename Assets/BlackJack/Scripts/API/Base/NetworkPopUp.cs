public static class NetworkPopUp
{
    public static void ShowPopUp(string heading,string message)
    {
        PopUpController.ShowPopUp(new PopContent(heading,message),new ButtonContent("Close",PopUpController.ClosePopUp));
    }
    
}