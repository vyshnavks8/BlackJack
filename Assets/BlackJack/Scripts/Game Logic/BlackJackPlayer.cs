using System;
using System.Collections.Generic;
using RedDevil.PlayingCards;
using UnityEngine;

[Serializable]
public class BlackJackPlayer
{
    public PlayerPosition playerPosition;
    public BlackJackPlayerUI blackJackPlayerUI;
    public Transform cardPosition => blackJackPlayerUI.transform;
    public List<Card> cards = new();
    public event Action<int> OnScoreChanged;
    public int Score { get; private set; }
    public int BetAmount { get; private set; }
    public float TimeAllowed { get; private set; }
    public PlayerType PlayerType { get; private set; }

    public bool IsSoftTotal()
    {
        foreach (var card in cards)
        {
            if (card.Rank == CardRank.Ace && cards.Count == 2)
            {
                return true;
            }
        }

        return false;
    }

    public Card GetTopCard()
    {
        return cards.Count > 0 ? cards[0] : null;
    }

    public void AddCard(Card card, bool visible = true)
    {
        cards.Add(card);
        blackJackPlayerUI.AddCard(card, visible);
        if (!visible) return;
        ShowScore();
    }

    public void SetBet(int amount, Action callback = null)
    {
        BetAmount = amount;
        blackJackPlayerUI.PlaceChip(BetAmount, callback);
    }

    private void ShowScore()
    {
        Score = BlackJackGameUtility.CalculatePlayerScore(cards);
        //blackJackPlayerUI.ShowScore(Score.ToString());
        OnScoreChanged?.Invoke(Score);
    }

    public void ResetData()
    {
        StopTimer();
        blackJackPlayerUI.RemoveAllCards();
        blackJackPlayerUI.RemoveAllChips();
        blackJackPlayerUI.ShowScore(string.Empty);
        blackJackPlayerUI.ShowStatus(string.Empty);
        cards.Clear();
        Score = 0;
    }

    public void RevealCards()
    {
        blackJackPlayerUI.RevealCard(ShowScore);
    }

    public PlayerStatus GetStatus()
    {
        return Score switch
        {
            21 => PlayerStatus.Won,
            > 21 => PlayerStatus.Busted,
            _ => PlayerStatus.None
        };
    }

    public void ShowStatus()
    {
        switch (GetStatus())
        {
            case PlayerStatus.None:
                blackJackPlayerUI.ShowStatus(string.Empty);
                break;
            case PlayerStatus.Won:
                blackJackPlayerUI.ShowStatus("Won!");
                break;
            case PlayerStatus.Busted:
                blackJackPlayerUI.ShowStatus("Busted!");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void ShowProfile(bool show)
    {
        blackJackPlayerUI.ShowProfile(show);
    }

    public void StartTimer(Action callback = null)
    {
        blackJackPlayerUI.StartTimer(callback);
    }

    public void StopTimer()
    {
        blackJackPlayerUI.StopTimer();
    }

    public void SetData(PlayerType playerType, float i)
    {
        PlayerType = playerType;
        TimeAllowed = i;
        blackJackPlayerUI.SeData(playerType, i);
    }
}