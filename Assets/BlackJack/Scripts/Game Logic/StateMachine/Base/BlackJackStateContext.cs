using System;
using System.Collections.Generic;
using System.Linq;
using RedDevil.PlayingCards;
using UnityEngine;

public class BlackJackStateContext : MonoBehaviour
{
    public BlackJackBotManager botManager;
    public CardPlacer cardPlacer;
    public GameMenuUI GameMenu;
    public Deck deck;
    public List<BlackJackPlayer> currentPlayers = new();
    public List<BlackJackPlayer> removedPlayers = new();
    public List<BlackJackPlayer> totalPlayers = new();
    public BlackJackPlayer Dealer;
    public int playerCounter;


    public bool IsMaxPlayerCounter() => playerCounter >= currentPlayers.Count;

    public void PlacePlayerCard(Action<bool> completed, bool doIncrement = false)
    {
        if (playerCounter < currentPlayers.Count)
        {
            var card = deck.DrawTopCard();
            var blackJackPlayer = currentPlayers[playerCounter];
            cardPlacer.MoveCardAnimation(blackJackPlayer.cardPosition, () =>
            {
                if (doIncrement)
                {
                    playerCounter += 1;
                }

                blackJackPlayer.AddCard(card);
                completed?.Invoke(true);
            });
        }
        else
        {
            completed?.Invoke(false);
        }
    }

    public void PlacePlayerChip(int amount, Action<bool> completed, bool doIncrement = false)
    {
        if (playerCounter < currentPlayers.Count)
        {
            var blackJackPlayer = currentPlayers[playerCounter];
            blackJackPlayer.SetBet(amount, () =>
            {
                if (doIncrement)
                {
                    playerCounter += 1;
                }

                completed?.Invoke(true);
            });
        }
        else
        {
            completed?.Invoke(false);
        }
    }

    public void PlaceDealerCard(bool visible, Action<bool> completed = null)
    {
        var card = deck.DrawTopCard();
        var dealer = Dealer;
        cardPlacer.MoveCardAnimation(dealer.cardPosition, () =>
        {
            dealer.AddCard(card, visible);
            completed?.Invoke(true);
        });
    }

    public void RevealDealerCard()
    {
        Dealer.RevealCards();
    }

    public List<BlackJackPlayer> GetWonPlayer()
    {
        return currentPlayers.Where(player => player.Score == 21).ToList();
    }

    public void AddToRemovedPlayer(BlackJackPlayer player)
    {
        removedPlayers.Add(player);
    }

    public void RemoveFromCurrentPlayer(BlackJackPlayer player)
    {
        currentPlayers.Remove(player);
    }

    public void ResetData()
    {
        cardPlacer.StopAnimation();
        ResetPlayer(totalPlayers);
        ResetPlayer(currentPlayers);
        ResetPlayer(removedPlayers);
        removedPlayers.Clear();
        currentPlayers.Clear();
        totalPlayers.Clear();
        playerCounter = 0;
        deck = null;
        Dealer = null;
    }

    private void ResetPlayer(List<BlackJackPlayer> players)
    {
        foreach (var player in players)
        {
            player.ResetData();
        }

        Dealer.ResetData();
    }

    public PlayerStatus CheckCurrentPlayerStatus()
    {
        return currentPlayers[playerCounter].GetStatus();
    }

    public void ShowCurrentPlayerStatus()
    {
        currentPlayers[playerCounter].ShowStatus();
    }

    public void StartPlayerTimer(Action completed)
    {
        if (playerCounter >= currentPlayers.Count) return;
        currentPlayers[playerCounter].StartTimer(completed);
    }

    public void StopPlayerTimer()
    {
        if (playerCounter >= currentPlayers.Count) return;
        currentPlayers[playerCounter].StopTimer();
    }

    public void SetPlayer(BlackJackPlayer dealer, List<BlackJackPlayer> blackJackPlayers)
    {
        Dealer = dealer;
        currentPlayers = blackJackPlayers;
        totalPlayers = blackJackPlayers;
    }

    public bool CheckBotBet(Action<int> bet)
    {
        var blackJackPlayer = currentPlayers[playerCounter];
        if (blackJackPlayer.PlayerType == PlayerType.Bot)
        {
            botManager.DoBet(blackJackPlayer, bet);
            return true;
        }
        return false;
    }

    public bool CheckBotPlay(Action<PlayerChoice> playerChoice)
    {
        var blackJackPlayer = currentPlayers[playerCounter];
        if (blackJackPlayer.PlayerType == PlayerType.Bot)
        {
            botManager.DoPlay(Dealer,blackJackPlayer, playerChoice);
            return true;
        }
        return false;
    }

    public void StopCheckBot()
    {
        botManager.ResetBot();
    }
}