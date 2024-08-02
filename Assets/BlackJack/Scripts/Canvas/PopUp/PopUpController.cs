using System;

public static class PopUpController
{
    public static event Action OnClosePopUp;
    public static event Action<PopContent, ButtonContent, ButtonContent> OnShowPopUpDual;
    public static event Action<PopContent, ButtonContent, ButtonContent, TimedContent> OnShowPopUpDualTimed;
    public static event Action<PopContent, ButtonContent> OnShowPopUpSingle;
    public static event Action<PopContent> OnShowPopUpNoButton;


    public static void ShowPopUp(PopContent popContent, ButtonContent buttonContentA, ButtonContent buttonContentB)
    {
        OnShowPopUpDual?.Invoke(popContent, buttonContentA, buttonContentB);
    }

    public static void ShowPopUpTimed(PopContent popContent, ButtonContent buttonContentA, ButtonContent buttonContentB,
        TimedContent timedContent)
    {
        OnShowPopUpDualTimed?.Invoke(popContent, buttonContentA, buttonContentB, timedContent);
    }

    public static void ShowPopUp(PopContent popContent, ButtonContent buttonContent)
    {
        OnShowPopUpSingle?.Invoke(popContent, buttonContent);
    }

    public static void ShowPopUp(PopContent popContent)
    {
        OnShowPopUpNoButton?.Invoke(popContent);
    }

    public static void ClosePopUp()
    {
        OnClosePopUp?.Invoke();
    }
}