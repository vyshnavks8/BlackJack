using System;

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