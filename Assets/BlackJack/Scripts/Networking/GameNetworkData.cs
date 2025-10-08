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
        byte[] playerIcon;
        if (string.IsNullOrEmpty(AppData.username))
        {
            playerName = "Unknown"+Random.Range(1000,9999);
            playerIcon = null;
        }
        else
        {
            playerName = AppData.username;
            playerIcon = AppData.profileIcon.texture.EncodeToPNG();
        }
        NetworkManager.SetLocalPlayerProperties(GameNetworkKey.PlayerName,playerName );
        NetworkManager.SetLocalPlayerProperties(GameNetworkKey.PlayerIcon,playerIcon );
    }

    public static (string playerName,Sprite playerIcon) GetPlayerData(Player player)
    {
       var pName=(string) NetworkManager.GetPlayerProperties(player, GameNetworkKey.PlayerName);
       var pIconBytes=(byte[]) NetworkManager.GetPlayerProperties(player, GameNetworkKey.PlayerIcon);
       if (pIconBytes is not { Length: > 0 }) return (pName, null);
       var tex = new Texture2D(2, 2);
       tex.LoadImage(pIconBytes); 
       var iconSprite = BlackjackUtils.GetSprite(tex);
       return (pName,iconSprite);
    }

    
}