using System;
using System.Collections.Generic;
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
   [SerializeField] private List<InfoData> data=new();
   [SerializeField] private InfoCanvas infoCanvas;
    private void OnEnable()
    {
        InfoController.OnUpdateInfo += OnUpdateInfo;
       
    }
    private void OnUpdateInfo(string id, CanvasBase canvasBase)
    {
        foreach (var infoData in data.Where(infoData => infoData.id == id))
        {
            infoCanvas.UpdateInfo(infoData.heading,infoData.info,canvasBase);
        }
    }


    private void OnDisable()
    {
        InfoController.OnUpdateInfo -= OnUpdateInfo;
    }
}