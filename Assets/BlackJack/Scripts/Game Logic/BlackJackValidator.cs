using RedDevil.PlayingCards;

public static class BlackJackValidator
{
    public static int GetCardValue(Card card)
    {
        return card.Rank switch
        {
            CardRank.Ace => 0,
            CardRank.Two => 2,
            CardRank.Three => 3,
            CardRank.Four => 4,
            CardRank.Five => 5,
            CardRank.Six => 6,
            CardRank.Seven => 7,
            CardRank.Eight => 8,
            CardRank.Nine => 9,
            CardRank.Ten => 10,
            CardRank.Jack => 10,
            CardRank.Queen => 10,
            CardRank.King => 10,
            _ => 0
        };
    }

    public static int UpdateCardAceValue(int score)
    {
        var scoreA = score + 11;
        var scoreB = score + 1;
        if (scoreA == 21)
        {
            return scoreA;
        }

        if (scoreB == 21)
        {
            return scoreB;
        }

        if (scoreB < 21 && scoreA > 21)
        {
            return scoreB;
        }
        return scoreA;
    }
}