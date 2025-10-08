using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public static class NetworkManager
{
    private static readonly Hashtable CustomTable = new();
    public static bool IsMasterClient => PhotonNetwork.IsMasterClient;
    public static Player LocalPlayer => PhotonNetwork.LocalPlayer;
    public static Room CurrentRoom => PhotonNetwork.CurrentRoom;

    public static string GenerateRoomCode()
    {
        var random = Random.Range(11111, 99999);
        return random.ToString();
    }

    public static void ConnectUsingSettings()
    {
        if (PhotonNetwork.IsConnected) return;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = Application.version;
    }

    public static void Disconnect()
    {
        if (!PhotonNetwork.IsConnected) return;
        PhotonNetwork.Disconnect();
    }


    public static void JoinRandomOrCreateRoom(byte maxPlayersPerRoom)
    {
        if (!PhotonNetwork.IsConnected) return;
        PhotonNetwork.JoinRandomOrCreateRoom(null,
            maxPlayersPerRoom,
            MatchmakingMode.SerialMatching,
            null,
            null,
            null,
            new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public static void CreateRoom(string roomName, byte maxPlayersPerRoom)
    {
        if (!PhotonNetwork.IsConnected) return;
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public static void JoinRoom(string roomName)
    {
        if (!PhotonNetwork.IsConnected) return;
        PhotonNetwork.JoinRoom(roomName);
    }

    public static void LeaveRoom()
    {
        if (!PhotonNetwork.IsConnected || !PhotonNetwork.InRoom) return;
        PhotonNetwork.LeaveRoom();
    }

    public static void JoinLobby(string lobbyName)
    {
        if (!PhotonNetwork.IsConnected) return;
        var lobby = new TypedLobby(lobbyName, LobbyType.Default);
        PhotonNetwork.JoinLobby(lobby);
    }

    public static void LeaveLobby()
    {
        if (!PhotonNetwork.IsConnected) return;
        PhotonNetwork.LeaveLobby();
    }

    public static void SetLocalPlayerProperties(string key, object data)
    {
        CustomTable[key] = data;
        LocalPlayer.SetCustomProperties(CustomTable);
    }

    public static object GetPlayerProperties(Player player, string key)
    {
        return player.CustomProperties[key];
    }

    public static void RaiseEvent(object data, byte eventCode, ReceiverGroup group, bool reliable = true)
    {
        if (reliable)
        {
            SendNetworkEventReliable(data, eventCode, group);
        }
        else
        {
            SendNetworkEventUnReliable(data, eventCode, group);
        }
    }

    public static void RaiseEvent(object data, byte eventCode, int[] targetActors, bool reliable = true)
    {
        if (reliable)
        {
            SendNetworkEventReliable(data, eventCode, targetActors);
        }
        else
        {
            SendNetworkEventUnReliable(data, eventCode, targetActors);
        }
    }

    private static void SendNetworkEventReliable(object data, byte eventCode, ReceiverGroup group)
    {
        if (!PhotonNetwork.IsConnected) return;
        var raiseEventOptions = new RaiseEventOptions { Receivers = group };
        PhotonNetwork.RaiseEvent(eventCode, data, raiseEventOptions, SendOptions.SendReliable);
    }

    private static void SendNetworkEventReliable(object data, byte eventCode, int[] targetActors)
    {
        if (!PhotonNetwork.IsConnected) return;
        var raiseEventOptions = new RaiseEventOptions { TargetActors = targetActors };
        PhotonNetwork.RaiseEvent(eventCode, data, raiseEventOptions, SendOptions.SendReliable);
    }

    private static void SendNetworkEventUnReliable(object data, byte eventCode, ReceiverGroup group)
    {
        if (!PhotonNetwork.IsConnected) return;
        var raiseEventOptions = new RaiseEventOptions { Receivers = group };
        PhotonNetwork.RaiseEvent(eventCode, data, raiseEventOptions, SendOptions.SendUnreliable);
    }

    private static void SendNetworkEventUnReliable(object data, byte eventCode, int[] targetActors)
    {
        if (!PhotonNetwork.IsConnected) return;
        var raiseEventOptions = new RaiseEventOptions { TargetActors = targetActors };
        PhotonNetwork.RaiseEvent(eventCode, data, raiseEventOptions, SendOptions.SendUnreliable);
    }
}