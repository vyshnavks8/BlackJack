using System;
using DG.Tweening;
using UnityEngine;

public class CardPlacer : MonoBehaviour
{
    [SerializeField] private RectTransform demoCard;
    [SerializeField, Range(0.1f, 3.0f)] private float duration = 1;
    private Tweener positionTween;
    private Tweener rotateTween;
    private Vector3 position;
    private Quaternion rotation;

    private void Start()
    {
        position = demoCard.position;
        rotation = demoCard.rotation;
    }

    public void MoveCardAnimation(Transform points, Action onComplete)
    {
        positionTween = demoCard.DOMove(points.position, duration).SetLink(demoCard.gameObject).OnComplete(() =>
        {
            demoCard.transform.localPosition = Vector3.zero;
            demoCard.transform.localRotation = Quaternion.identity;
            onComplete?.Invoke();
        });
        rotateTween=  demoCard.DORotate(points.rotation.eulerAngles, duration).SetLink(demoCard.gameObject);
    }

    public void MoveCardToOrigin(Transform points, Action onComplete)
    {
        demoCard.transform.position = points.position;
        demoCard.transform.rotation = points.rotation;
        positionTween = demoCard.transform.DOMove(position, duration).SetLink(demoCard.gameObject).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
        rotateTween=  demoCard.DORotate(rotation.eulerAngles, duration).SetLink(demoCard.gameObject);
    }

    public void StopAnimation()
    {
        positionTween.Kill();
        rotateTween.Kill();
        demoCard.transform.position = position;
        demoCard.transform.rotation = rotation;
    }
}