public static class GameNetworkExtensionHelper
{
    public static bool IsLocalNetworkPlayer(this BlackJackPlayer player)
    {
        return player.NetworkID == NetworkManager.LocalPlayer.ActorNumber;
    }
}