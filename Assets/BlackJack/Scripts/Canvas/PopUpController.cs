using System;

public struct PopContent
{
    public readonly string Heading;
    public readonly string Data;

    public PopContent(string heading, string data)
    {
        Heading = heading;
        Data = data;
    }
}

public struct ButtonContent
{
    public readonly string ButtonText;
    public readonly Action ButtonCallback;

    public ButtonContent(string buttonText, Action buttonCallback)
    {
        ButtonText = buttonText;
        ButtonCallback = buttonCallback;
    }
}

public struct TimedContent
{
    public readonly int Time;
    public readonly Action TimedCallback;

    public TimedContent(int timeValue, Action timedCallback)
    {
        Time = timeValue;
        TimedCallback = timedCallback;
    }
}

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