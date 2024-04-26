using System;
using UnityEngine;

public abstract class CanvasBase : MonoBehaviour
{
    public event Action<CanvasBase> SetCanvasActive;

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
}