public static class AIBotUtility
{
    public static PlayerChoice GetHardChoice(int dealerScore, int playerScore)
    {
        switch (playerScore)
        {
            case >= 5 and <= 11:
                return PlayerChoice.Hit;
            case 12:
                return dealerScore is >= 4 and <= 6 ? PlayerChoice.Stand : PlayerChoice.Hit;
            case <= 16:
            {
                if (dealerScore is >= 2 and <= 6)
                {
                    return PlayerChoice.Stand;
                }
                if (playerScore is 15 or 16 && dealerScore is 0 or 10)
                {
                    return PlayerChoice.Stand;
                }
                return PlayerChoice.Hit;
            }
            default:
                return PlayerChoice.Stand;
        }
    }

    public static PlayerChoice GetSoftChoice(int dealerScore, int playerScore)
    {
        switch (playerScore)
        {
            case >= 2 and <= 6:
                return PlayerChoice.Hit;
            case 7:
                return dealerScore is >= 2 and <= 8 ? PlayerChoice.Stand : PlayerChoice.Hit;
            default:
                return PlayerChoice.Stand;
        }
    }
}