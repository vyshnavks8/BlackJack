using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MasterCanvas : MonoBehaviour
{
    [SerializeField] private float transitionDuration = 0.3f;
    [SerializeField] private bool init;
    [SerializeField] private CanvasBase startingCanvas;
    [SerializeField] private List<CanvasBase> canvasBase = new();
    private CanvasBase currentCanvas;
    private CanvasBase currentOverlayCanvas;

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
            canvas.SetCanvasOverlay += SetCanvasOverlay;
        }
    }

    private void OnDisable()
    {
        foreach (var canvas in canvasBase)
        {
            canvas.SetCanvasActive -= SetCanvasActive;
            canvas.SetCanvasOverlay -= SetCanvasOverlay;
        }
    }



    private void SetCanvasActive(CanvasBase canvas)
    {
        if (currentCanvas.transform.GetSiblingIndex() < canvas.transform.GetSiblingIndex())
        {
            canvas.transform.DOLocalMoveX(4000, 0f).OnComplete(() =>
            {
                canvas.gameObject.SetActive(true);
                canvas.transform.DOLocalMoveX(0, transitionDuration).SetEase(Ease.OutQuad)
                    .OnComplete(() => SetCurrentCanvas(canvas));
            });
        }
        else
        {
            canvas.gameObject.SetActive(true);
            currentCanvas.transform.DOLocalMoveX(4000, transitionDuration).SetEase(Ease.InQuad)
                .OnComplete(() => SetCurrentCanvas(canvas));
        }
    }

    private void SetCurrentCanvas(CanvasBase canvas)
    {
        if (currentCanvas != null)
        {
            currentCanvas.GetRayCaster.enabled = true;
            currentCanvas.gameObject.SetActive(false);
        }
        currentCanvas = canvas;
        CheckOverlayVisible();
    }

    private void CheckOverlayVisible()
    {
        if (currentOverlayCanvas == null) return;
        if (currentCanvas.isOverOverlay)
        {
            currentOverlayCanvas.GetRayCaster.enabled = false;
            currentCanvas.GetRayCaster.enabled = true;
        }
        else
        {
            currentOverlayCanvas.GetRayCaster.enabled = true;
            currentCanvas.GetRayCaster.enabled = false;
        }
    }
    private void SetCanvasOverlay(CanvasBase canvas, bool active)
    {
        currentCanvas.GetRayCaster.enabled = !active;
        currentOverlayCanvas = active ? canvas : null;
        canvas.gameObject.SetActive(active);
    }
}