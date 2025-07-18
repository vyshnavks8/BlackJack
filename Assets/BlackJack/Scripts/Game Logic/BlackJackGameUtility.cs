using System;
using System.Collections.Generic;
using System.Linq;
using RedDevil.PlayingCards;

public static class BlackJackGameUtility
{
    public static List<BlackJackPlayer> GetPlayers(int count, BlackJackPlayer[] players)
    {
        var positionsToAdd = new HashSet<PlayerPosition>();
        switch (count)
        {
            case 1:
                positionsToAdd.Add(PlayerPosition.Bottom);
                break;
            case 2:
                positionsToAdd.Add(PlayerPosition.BottomRight);
                positionsToAdd.Add(PlayerPosition.Bottom);
                break;
            case 3:
                positionsToAdd.Add(PlayerPosition.BottomRight);
                positionsToAdd.Add(PlayerPosition.Bottom);
                positionsToAdd.Add(PlayerPosition.BottomLeft);
                break;
            case 4:
                positionsToAdd.Add(PlayerPosition.TopRight);
                positionsToAdd.Add(PlayerPosition.BottomRight);
                positionsToAdd.Add(PlayerPosition.Bottom);
                positionsToAdd.Add(PlayerPosition.BottomLeft);
                break;
            case 5:
                positionsToAdd.Add(PlayerPosition.TopRight);
                positionsToAdd.Add(PlayerPosition.BottomRight);
                positionsToAdd.Add(PlayerPosition.Bottom);
                positionsToAdd.Add(PlayerPosition.BottomLeft);
                positionsToAdd.Add(PlayerPosition.TopLeft);
                break;
            case 6 :
                positionsToAdd.Add(PlayerPosition.Top);
                positionsToAdd.Add(PlayerPosition.TopRight);
                positionsToAdd.Add(PlayerPosition.BottomRight);
                positionsToAdd.Add(PlayerPosition.Bottom);
                positionsToAdd.Add(PlayerPosition.BottomLeft);
                positionsToAdd.Add(PlayerPosition.TopLeft);
                break;
                
        }
        return players.Where(player => positionsToAdd.Contains(player.playerPosition)).ToList();
    }

    public static int GetPlayerIndex(PlayerPosition playerPosition)
    {
        switch (playerPosition)
        {
            case PlayerPosition.Top:
                return 0;
            case PlayerPosition.TopRight:
                return 1;
            case PlayerPosition.BottomRight:
                return 2;
            case PlayerPosition.Bottom:
               return 3;
            case PlayerPosition.BottomLeft:
                return 4;
            case PlayerPosition.TopLeft:
                return 5;
        }
        return -1;
    }
    public static int CalculatePlayerScore(List<Card> cards)
    {
        var score = cards.Sum(BlackJackValidator.GetCardValue);

        return cards.Where(card => card.Rank == CardRank.Ace).Aggregate(score, (current, _) => BlackJackValidator.UpdateCardAceValue(current));
    }
}