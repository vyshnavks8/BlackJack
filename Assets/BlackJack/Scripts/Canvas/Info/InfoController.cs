using System;

public static class InfoController
{
    public static event Action<string, CanvasBase> OnUpdateInfo;
    public static void UpdateInfo(string id, CanvasBase canvasBase)
    {
        OnUpdateInfo?.Invoke(id,canvasBase);
    }
}