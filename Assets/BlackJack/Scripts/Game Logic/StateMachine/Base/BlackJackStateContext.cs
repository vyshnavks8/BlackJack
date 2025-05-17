using System;
using System.Collections.Generic;
using System.Linq;
using RedDevil.PlayingCards;
using UnityEngine;
using UnityEngine.Serialization;

public class BlackJackStateContext : MonoBehaviour
{
    public BlackJackBotManager botManager;
    public CardPlacer cardPlacer;
    public ChipPlacer chipPlacer;
    public GameMenuUI GameMenu;
    public Deck deck;
    public List<BlackJackPlayer> currentPlayers = new();
    public List<BlackJackPlayer> removedPlayers = new();
    public List<BlackJackPlayer> totalPlayers = new();

    public BlackJackPlayer Dealer { get; private set; }
    public int DealerIndex { get; private set; }
    public int PlayerCounter;// { private set; get; }
    public bool FirstGame { get; private set; }
    public int cardPlaceCounter;
    public bool IsCurrentPlayerBot => currentPlayers[PlayerCounter].PlayerType == PlayerType.Bot;
    public bool IsMaxPlayerReached => passCounter > currentPlayers.Count - 2;
    public int passCounter;

    public void DiscardPlayerCard(Action completedRound)
    {
        var blackJackPlayer = currentPlayers[PlayerCounter];
        blackJackPlayer.blackJackPlayerUI.RemoveAllCards();
        cardPlacer.MoveCardToOrigin(blackJackPlayer.cardPosition, () => { completedRound?.Invoke(); });
    }

    public void DiscardDealerCard(Action completedRound)
    {
        Dealer.blackJackPlayerUI.RemoveAllCards();
        cardPlacer.MoveCardToOrigin(Dealer.cardPosition, () => { completedRound?.Invoke(); });
    }

    public void PlacePlayerCard(Action<bool> completedRound, bool doIncrement = false, bool visible = true)
    {
        if (cardPlaceCounter < currentPlayers.Count - 1)
        {
            var card = deck.DrawTopCard();
            var blackJackPlayer = currentPlayers[PlayerCounter];
            cardPlacer.MoveCardAnimation(blackJackPlayer.cardPosition, () =>
            {
                if (doIncrement)
                {
                    IncrementNextPlayer();
                    cardPlaceCounter += 1;
                }

                blackJackPlayer.AddCard(card, visible);
                completedRound?.Invoke(true);
            });
        }
        else
        {
            completedRound?.Invoke(false);
        }
    }

    public void IncrementNextPlayer()
    {
        PlayerCounter += 1;
        if (PlayerCounter >= currentPlayers.Count)
        {
            PlayerCounter = 0;
        }
    }

    public void PlacePlayerChip(int amount, Action<bool> completed, bool doIncrement = false)
    {
        if (PlayerCounter < currentPlayers.Count)
        {
            var blackJackPlayer = currentPlayers[PlayerCounter];
            blackJackPlayer.SetBet(amount, () =>
            {
                if (doIncrement)
                {
                    PlayerCounter += 1;
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
        chipPlacer.StopAnimation();
        ResetPlayer(totalPlayers);
        ResetPlayer(currentPlayers);
        ResetPlayer(removedPlayers);
        removedPlayers.Clear();
        currentPlayers.Clear();
        totalPlayers.Clear();
        PlayerCounter = 0;
        passCounter = 0;
        FirstGame = false;
        deck = null;
        Dealer = null;
    }

    private void ResetPlayer(List<BlackJackPlayer> players)
    {
        foreach (var player in players)
        {
            player.ResetData();
        }

        Dealer?.ResetData();
    }

    public PlayerStatus CheckCurrentPlayerStatus()
    {
        return currentPlayers[PlayerCounter].GetStatus();
    }

    public void ShowCurrentPlayerStatus()
    {
        currentPlayers[PlayerCounter].ShowStatus();
    }

    public void StartPlayerTimer(Action completed)
    {
        if (PlayerCounter >= currentPlayers.Count) return;
        currentPlayers[PlayerCounter].StartTimer(completed);
    }


    public void StopPlayerTimer()
    {
        if (PlayerCounter >= currentPlayers.Count) return;
        currentPlayers[PlayerCounter].StopTimer();
    }

    public void SetPlayer(List<BlackJackPlayer> blackJackPlayers)
    {
        currentPlayers = blackJackPlayers;
        totalPlayers = blackJackPlayers;
    }

    public void SetDealer(BlackJackPlayer dealer, int index)
    {
        Dealer = dealer;
        DealerIndex = index;
        Dealer.blackJackPlayerUI.SetDealerStyle();
    }

    public BlackJackPlayer GetCurrentPlayer()
    {
        return currentPlayers[PlayerCounter];
    }

    public bool CheckBotBet(int amount,Action<int> bet)
    {
        var blackJackPlayer = currentPlayers[PlayerCounter];
        if (blackJackPlayer.PlayerType == PlayerType.Bot)
        {
            botManager.DoBetAmount(blackJackPlayer,amount, bet);
            return true;
        }

        return false;
    }

    public bool CheckBotHitOrStand(Action<PlayerChoice> playerChoice)
    {
        var blackJackPlayer = currentPlayers[PlayerCounter];
        if (blackJackPlayer.PlayerType == PlayerType.Bot)
        {
            botManager.DoHitOrStand(Dealer, blackJackPlayer, playerChoice);
            return true;
        }

        return false;
    }

    public bool CheckBotShowOrMuck(Action<PlayerChoice> playerChoice)
    {
        var blackJackPlayer = currentPlayers[PlayerCounter];
        if (blackJackPlayer.PlayerType == PlayerType.Bot)
        {
            botManager.DoShowOrMuck(Dealer, blackJackPlayer, playerChoice);
            return true;
        }

        return false;
    }

    public bool CheckBotBetOrPass(Action<PlayerChoice> playerChoice)
    {
        var blackJackPlayer = currentPlayers[PlayerCounter];
        if (blackJackPlayer.PlayerType == PlayerType.Bot)
        {
            botManager.DoBetOrPass(blackJackPlayer, playerChoice);
            return true;
        }

        return false;
    }

    public void StopCheckBot()
    {
        botManager.ResetBot();
    }

    public void SetFirstGame(bool firstGame)
    {
        FirstGame = firstGame;
    }

    public void SetAllPlayerStyle()
    {
        foreach (var player in currentPlayers)
        {
            player.blackJackPlayerUI.SetPlayerStyle();
        }
    }

    public void MakeDealerWinner(BlackJackPlayer dealer, BlackJackPlayer player, Action onComplete)
    {
        var start = player.chipPosition;
        var end = dealer.chipPosition;
        player.blackJackPlayerUI.RemoveAllChips();
        chipPlacer.MoveChip(player.BetAmount, start, end, () =>
        {
            dealer.UpdateBetAmount(player.BetAmount + dealer.BetAmount);
            player.UpdateBetAmount(0);
            onComplete?.Invoke();
        });
    }

    public void MakePlayerWinner(BlackJackPlayer dealer, BlackJackPlayer player, Action onComplete)
    {
        var start = dealer.chipPosition;
        var end = player.chipPosition;
        dealer.UpdateBetAmount(dealer.BetAmount - player.BetAmount);
        chipPlacer.MoveChip(player.BetAmount, start, end, () =>
        {
            player.UpdateBetAmount(2 * player.BetAmount);
            onComplete?.Invoke();
        });
    }

    public void SetPlayerCounter(int counter)
    {
        PlayerCounter = counter;
    }

    public void IncrementPassCounter()
    {
        passCounter += 1;
    }

    public void ShowInfo(string info)
    {
        if (IsCurrentPlayerBot)
        {
            GameMenu.ShowInfoText(info.ToUpper());
        }
    }
}