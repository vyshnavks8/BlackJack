using System;
using DG.Tweening;
using UnityEngine;

public class ChipPlacer : MonoBehaviour
{
    [SerializeField] private ChipUI chipUI;
    [SerializeField] private float duration = 1.5f;
Tweener tween;
    public void MoveChip(int value, RectTransform startPos, RectTransform endPos, Action OnComplete)
    {
        chipUI.SetChipValue(value);
        chipUI.transform.position = startPos.position;
        chipUI.gameObject.SetActive(true);
        tween=  chipUI.MoveTo(endPos, () =>
        {
            chipUI.gameObject.SetActive(false);
            OnComplete?.Invoke();
        },1);
    }
    public void StopAnimation()
    {
        tween.Kill();
    }
}