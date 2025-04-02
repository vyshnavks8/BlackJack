using Photon.Realtime;
using UnityEngine;

public enum NetworkGameType
{
    None,
    Random,
    Friends,
}

public enum PrivateGameType
{
    None,
    Create,
    Join
}

public static class GameNetworkData
{
    private static NetworkGameType gameType;
    private static PrivateGameType privateGameType;
    public static NetworkGameType GetGameType => gameType;

    public static void SetGameType(NetworkGameType type)
    {
        gameType = type;
    }

    public static void SetPrivateGameType(PrivateGameType type)
    {
        privateGameType = type;
    }

    public static void SetPlayerData()
    {
        string playerName;
        if (string.IsNullOrEmpty(AppData.username))
        {
            playerName = "Unknown"+Random.Range(1000,9999);
        }
        else
        {
            playerName = AppData.username;
        }
        NetworkManager.SetLocalPlayerProperties(GameNetworkKey.PlayerName,playerName );
    }

    public static (string playerName,Sprite playerIcon) GetPlayerData(Player player)
    {
       var pName=(string) NetworkManager.GetPlayerProperties(player, GameNetworkKey.PlayerName);
       return (pName,null);
    }
}