using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class InfoData
{
    public string id;
    public string heading;
    [TextArea(5, 20)] public string info;
}

public class InfoManager : MonoBehaviour
{
   [SerializeField] private InfoHolder infoHolder;
   [SerializeField] private InfoCanvas infoCanvas;
    private void OnEnable()
    {
        InfoController.OnUpdateInfo += OnUpdateInfo;
       
    }
    private void OnUpdateInfo(string id, CanvasBase canvasBase)
    {
        foreach (var infoData in infoHolder.data.Where(infoData => infoData.id == id))
        {
            infoCanvas.UpdateInfo(infoData.heading,infoData.info,canvasBase);
        }
    }


    private void OnDisable()
    {
        InfoController.OnUpdateInfo -= OnUpdateInfo;
    }
}