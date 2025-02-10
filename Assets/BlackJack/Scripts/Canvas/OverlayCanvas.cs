using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public abstract class OverlayCanvas : MonoBehaviour
{
    [SerializeField] private float transitionDuration = 0.3f;
    [SerializeField] private GameObject pivot;
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
        pivot.transform.DOLocalMoveY(-4000, 0).OnComplete(() =>
        {
            pivot.SetActive(true);
            pivot.transform.DOLocalMoveY(0, transitionDuration).SetEase(Ease.OutQuad);
        });
    }

    public void HideOverlay()
    {
        pivot.transform.DOLocalMoveY(-4000, transitionDuration).SetEase(Ease.InQuad).OnComplete(() =>
        {
            pivot.SetActive(false);
            afterHide?.Invoke();
        });
    }
}