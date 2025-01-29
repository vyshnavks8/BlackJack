using DG.Tweening;
using UnityEngine;

public class LoadingCanvas : MonoBehaviour
{
    [SerializeField] private GameObject pivot;
    [SerializeField] private RectTransform loadingImage;
    private Tweener tween;

    public void ShowLoading(bool load)
    {
        pivot.SetActive(load);
        if (load)
        {
            tween = loadingImage.DORotate(new Vector3(0, 0, -360), 0.5f, RotateMode.LocalAxisAdd)
                .SetLoops(-1, LoopType.Incremental);
        }
        else
        {
            tween?.Kill();
        }
    }
}