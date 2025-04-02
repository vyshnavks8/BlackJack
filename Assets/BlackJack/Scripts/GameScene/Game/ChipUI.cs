using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChipUI : MonoBehaviour
{
    [SerializeField] private ChipDataSO chipDataSO;
    [SerializeField] private Image chipImage;
    [SerializeField] private TMP_Text chipValueText;
    [SerializeField] private bool autoHighlight;

    private void Awake()
    {
        if (!autoHighlight) return;
        chipImage.DOColor(Color.gray, 0.3f).SetLoops(-1, LoopType.Yoyo);
    }

    public void SetChipValue(int chipValue)
    {
        chipValueText.text = chipValue.ToString();
    }

    public void SetChipText(int chipValue)
    {
        chipValueText.text = chipValue.ToString();
    }

    public void MoveTo(RectTransform moveLocation, Action completed)
    {
        transform.DOMove(moveLocation.position, 0.5f).OnComplete(() => completed?.Invoke());
    }
}