public static class GameNetworkKey
{
    public const string PlayerName = "PLAYER_NAME";
    public const string PlayerIcon = "PLAYER_ICON";
}

public static class NetworkEventCode
{
    public const byte SyncPlayerList= 11;
    public const byte StartGame= 12;
    public const byte InitDeck= 13;
    public const byte PlaceBet= 14;
    public const byte PlaceChoice= 15;
    public const byte PlayerLeftGame= 16;
}