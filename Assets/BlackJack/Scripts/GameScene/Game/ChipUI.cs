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
    

    public Tweener MoveTo(RectTransform moveLocation, Action completed,float duration=0.5f)
    {
        return transform.DOMove(moveLocation.position, duration).OnComplete(() => completed?.Invoke());
    }
}