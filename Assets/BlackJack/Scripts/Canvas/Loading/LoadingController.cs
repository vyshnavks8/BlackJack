using System;

public static class LoadingController
{
    public static event Action<bool> OnLoading;

    public static void ShowLoading()
    {
        OnLoading?.Invoke(true);
    }

    public static void HideLoading()
    {
        OnLoading?.Invoke(false);
    }
}