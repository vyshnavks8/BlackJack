using System;
using DG.Tweening;
using UnityEngine;

public class CardPlacer : MonoBehaviour
{
    [SerializeField] private RectTransform demoCard;
    [SerializeField, Range(0.1f, 3.0f)] private float duration = 1;
    private Tweener tween;
    private Vector3 position;
    private Quaternion rotation;

    private void Start()
    {
        position = demoCard.position;
        rotation = demoCard.rotation;
    }

    public void MoveCardAnimation(Transform points, Action onComplete)
    {
        tween = demoCard.DOMove(points.position, duration).OnComplete(() =>
        {
            demoCard.transform.localPosition = Vector3.zero;
            demoCard.transform.localRotation = Quaternion.identity;
            onComplete?.Invoke();
        });
        demoCard.DORotate(points.rotation.eulerAngles, duration);
    }

    public void MoveCardToOrigin(Transform points, Action onComplete)
    {
        demoCard.transform.position = points.position;
        demoCard.transform.rotation = points.rotation;
        tween = demoCard.transform.DOMove(position, duration).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
        demoCard.DORotate(rotation.eulerAngles, duration);
    }

    public void StopAnimation()
    {
        tween.Kill();
        demoCard.transform.position = position;
        demoCard.transform.rotation = rotation;
    }
}