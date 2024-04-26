using System.Collections.Generic;
using UnityEngine;

public class MasterCanvas : MonoBehaviour
{
    [SerializeField] private bool init;
    [SerializeField] private CanvasBase startingCanvas;
    [SerializeField] private List<CanvasBase> canvasBase = new();
    private CanvasBase currentCanvas;

    private void Start()
    {
        if (init)
        {
            InitialiseCanvas();
        }
    }

    private void InitialiseCanvas()
    {
        currentCanvas = startingCanvas;
        currentCanvas.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        foreach (var canvas in canvasBase)
        {
            canvas.SetCanvasActive += SetCanvasActive;
        }
    }

    private void OnDisable()
    {
        foreach (var canvas in canvasBase)
        {
            canvas.SetCanvasActive -= SetCanvasActive;
        }
    }

    private void SetCanvasActive(CanvasBase canvas)
    {
        if (currentCanvas != null)
        {
            currentCanvas.gameObject.SetActive(false);
        }

        canvas.gameObject.SetActive(true);
        currentCanvas = canvas;
    }
}