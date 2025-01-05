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

    public void SetChipValue(int chipValue)
    {
        var colour = chipDataSO.GetChipColour(chipValue);
        chipImage.color = colour;
        chipValueText.text = chipValue.ToString();
    }

    public void MoveTo(RectTransform moveLocation,Action completed)
    {
        transform.DOMove(moveLocation.position, 0.5f).OnComplete(()=>completed?.Invoke());
    }
}