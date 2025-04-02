public static class GameNetworkKey
{
    public const string PlayerName = "PLAYER_NAME";
    public const string PlayerIcon = "PLAYER_ICON";
}

public static class NetworkEventCode
{
    public const byte SyncPlayerList= 11;
    public const byte StartGame= 12;
    public const byte PlaceBet= 13;
    public const byte InitDeck= 14;
}