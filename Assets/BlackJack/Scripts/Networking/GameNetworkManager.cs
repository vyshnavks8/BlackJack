using System;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class GameNetworkManager : MonoBehaviour
{
    [SerializeField] public NetworkPlayersCanvas networkPlayersCanvas;
    [SerializeField] public BlackJackManager blackJackManager;
    [SerializeField, HideInInspector] public List<int> playersList = new();
    public event Action SyncPlayerList;
    public event Action<Player> PlayerLeft;

    private void OnEnable()
    {
        NetworkCallbackManager.onMasterClientSwitched += OnMasterClientSwitched;
        NetworkCallbackManager.onDisconnected += OnDisconnect;
        NetworkCallbackManager.onJoinedRoom += OnJoinRoom;
        NetworkCallbackManager.onPlayerEnteredRoom += OnPlayerEnterRoom;
        NetworkCallbackManager.onPlayerLeftRoom += OnPlayerLeftRoom;
    }


    private void OnDisable()
    {
        NetworkCallbackManager.onMasterClientSwitched -= OnMasterClientSwitched;
        NetworkCallbackManager.onDisconnected -= OnDisconnect;
        NetworkCallbackManager.onJoinedRoom -= OnJoinRoom;
        NetworkCallbackManager.onPlayerEnteredRoom -= OnPlayerEnterRoom;
        NetworkCallbackManager.onPlayerLeftRoom -= OnPlayerLeftRoom;
    }

    private void OnMasterClientSwitched(Player player)
    {
        if (blackJackManager.gameStarted) return;
        networkPlayersCanvas.ShowCanvas(player.ActorNumber == NetworkManager.LocalPlayer.ActorNumber);
    }

    private void OnJoinRoom()
    {
        networkPlayersCanvas.ShowCanvas(NetworkManager.IsMasterClient);
        if (!NetworkManager.IsMasterClient) return;
        var data = GameNetworkData.GetPlayerData(NetworkManager.LocalPlayer);
        networkPlayersCanvas.AddPlayer(NetworkManager.LocalPlayer.ActorNumber, data);
        playersList.Add(NetworkManager.LocalPlayer.ActorNumber);
    }

    private void OnPlayerLeftRoom(Player player)
    {
       
        PlayerLeft?.Invoke(player);
        if (!NetworkManager.IsMasterClient) return;
        networkPlayersCanvas.RemovePlayer(player.ActorNumber);
        playersList.Remove(player.ActorNumber);
        SyncPlayerList?.Invoke();
    }

    private void OnPlayerEnterRoom(Player player)
    {
        if (!NetworkManager.IsMasterClient) return;
        var data = GameNetworkData.GetPlayerData(player);
        networkPlayersCanvas.AddPlayer(player.ActorNumber, data);
        playersList.Add(player.ActorNumber);
        SyncPlayerList?.Invoke();
    }

    private void OnDisconnect(DisconnectCause cause)
    {
        LoadingController.HideLoading();
        switch (cause)
        {
            case DisconnectCause.DisconnectByClientLogic:
                playersList.Clear();
                networkPlayersCanvas.HideCanvas();
                break;
        }
    }
}