using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public abstract class OverlayCanvas : MonoBehaviour
{
    [SerializeField] private float transitionDuration = 0.3f;
     [SerializeField] private Transform overlayPivot;
    [SerializeField] private UnityEvent afterHide;
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

    public void ShowOverlay()
    {
        overlayPivot.transform.DOLocalMoveY(-4000, 0).OnComplete(() =>
        {
            overlayPivot.gameObject.SetActive(true);
            overlayPivot.DOLocalMoveY(0, transitionDuration).SetEase(Ease.OutQuad);
        });
    }

    protected void HideOverlay()
    {
        overlayPivot.DOLocalMoveY(-4000, transitionDuration).SetEase(Ease.InQuad).OnComplete(() =>
        {
            overlayPivot.gameObject.SetActive(false);
            afterHide?.Invoke();
        });
    }
}