using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class CanvasBase : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster raycaster;
    protected CanvasBase transitionCanvas;
    public GraphicRaycaster GetRayCaster => raycaster;
    public event Action<CanvasBase> SetCanvasActive;
    public event Action<CanvasBase,bool> SetCanvasOverlay;
    public bool isOverOverlay;
    protected void OnValidate()
    {
        if (raycaster == null)
        {
          raycaster=  GetComponent<GraphicRaycaster>();
        }
    }

    protected virtual  void OnEnable()
    {
        AddListener();
    }
    protected virtual void OnDisable()
    {
        RemoveListener();
    }

    protected abstract void AddListener();
    protected abstract void RemoveListener();
    protected void OnSetCanvasActive(CanvasBase canvas)
    {
        SetCanvasActive?.Invoke(canvas);
    }
    protected void OnSetCanvasOverlay(CanvasBase canvas, bool active)
    {
        SetCanvasOverlay?.Invoke(canvas,active);
    }

    public void SetTransitionCanvas(CanvasBase canvas)
    {
        transitionCanvas=canvas;
    }
}