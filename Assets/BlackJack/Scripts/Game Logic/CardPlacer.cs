using System;
using DG.Tweening;
using UnityEngine;

public class CardPlacer : MonoBehaviour
{
    [SerializeField] private RectTransform demoCard;
    [SerializeField, Range(0.1f, 3.0f)] private float duration = 1;
    private Tweener tween;
    public void MoveCardAnimation(Transform points, Action onComplete)
    {
       tween= demoCard.DOMove(points.position, duration).OnComplete(() =>
        {
            demoCard.transform.localPosition = Vector3.zero;
            demoCard.transform.localRotation = Quaternion.identity;
            onComplete?.Invoke();
        });
        demoCard.DORotate(points.rotation.eulerAngles, duration);
    }

    public void StopAnimation()
    {
        tween.Kill();
    }
}