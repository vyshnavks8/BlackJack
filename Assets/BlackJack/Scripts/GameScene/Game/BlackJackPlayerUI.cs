using System;
using System.Collections.Generic;
using RedDevil.PlayingCards;
using TMPro;
using UnityEngine;

public class BlackJackPlayerUI : MonoBehaviour
{
    [SerializeField] private BlackJackPlayerProfileUI profileUI;
    [Header("CARD")] [SerializeField] private CardUI cardUI;
    [SerializeField] private RectTransform cardHolder;
    [Header("CHIP")] [SerializeField] private ChipUI chipUI;
    [SerializeField] private RectTransform chipHolder;
    [SerializeField] private RectTransform chipLocation;
    [SerializeField] private RectTransform chipOrigin;
    [Header("HAND")] [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text status;

    private readonly List<CardUI> cards = new();
    private readonly List<ChipUI> chips = new();
    private ChipUI chipInstance;
    public RectTransform ChipLocation => chipLocation;
    private int revealCount;
    public void AddCard(Card card, bool visible = true)
    {
        var cardInstance = Instantiate(cardUI, cardHolder);
        cardInstance.SetData(card, visible);
        cards.Add(cardInstance);
    }

    public void PlaceChip(int value, Action completed)
    {
        chipInstance = Instantiate(chipUI, chipHolder);
        chipInstance.transform.position = chipOrigin.position;
        chipInstance.SetChipValue(value);
        chipInstance.MoveTo(chipLocation, completed);
        chips.Add(chipInstance);
    }

    public void UpdateChip(int value)
    {
        chipInstance.SetChipValue(value);
    }

    public void MoveChip(RectTransform rect, Action completed)
    {
        chipInstance.MoveTo(rect, completed);
    }

    public void RevealCard(Action cardRevealed)
    {
        foreach (var cardInstance in cards)
        {
            cardInstance.RevealCard(() => OnCardsRevealed(cardRevealed));
        }
    }

    private void OnCardsRevealed(Action cardRevealed)
    {
        revealCount += 1;
        if (revealCount == cards.Count)
        {
            cardRevealed?.Invoke();
            revealCount = 0;
        }
    }

    public void RemoveAllCards()
    {
        foreach (var instance in cards)
        {
            Destroy(instance.gameObject);
        }

        cards.Clear();
    }

    public void RemoveAllChips()
    {
        foreach (var instance in chips)
        {
            Destroy(instance.gameObject);
        }

        chips.Clear();
    }


    public void ShowScore(string scoreValue)
    {
        score.text = scoreValue;
    }

    public void ShowStatus(string stat)
    {
        status.text = stat;
    }

    public void ShowProfile(bool show)
    {
        profileUI.gameObject.SetActive(show);
    }

    public void StartTimer(Action callback)
    {
        profileUI.StartTurn(callback);
    }

    public void StopTimer()
    {
        profileUI.StopTurn();
    }

    public void SeData(PlayerType playerType, float i)
    {
        profileUI.SetData(playerType, i);
    }

    public void SetDealerStyle()
    {
        profileUI.SetDealerStyle();
    }

    public void SetPlayerStyle()
    {
        profileUI.SetPlayerStyle();
    }

    public void EnablePlayerUI()
    {
        profileUI.EnablePlayer();
    }

    public void DisablePlayerUI()
    {
        profileUI.DisablePlayer();
    }
}