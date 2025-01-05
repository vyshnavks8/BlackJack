using System;
using DG.Tweening;
using PlayingCards;
using RedDevil.PlayingCards;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] private PlayingCardUIHolder playingCardUIHolder;
    [SerializeField] private Image cardImage;
    private Card CardData;
    private bool cardVisible;
    private const float duration = 0.5f;

    public void SetData(Card card, bool visible)
    {
        cardVisible = visible;
        CardData = card;
        cardImage.sprite = visible ? playingCardUIHolder.GetPlayingCardBack(CardData) : playingCardUIHolder.cardBackRed;
        if (!visible)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void RevealCard(Action finished = null)
    {
        if (cardVisible) return;
        transform
            .DOLocalRotate(new Vector3(0f, -90f, 0f), duration, RotateMode.LocalAxisAdd)
            .OnComplete(() => OnCompleteHalf(finished));
    }

    private void OnCompleteHalf(Action finished = null)
    {
        cardImage.sprite = playingCardUIHolder.GetPlayingCardBack(CardData);
        transform
            .DOLocalRotate(new Vector3(0f, -90f, 0f), duration, RotateMode.LocalAxisAdd)
            .OnComplete(() => finished?.Invoke());
    }
}