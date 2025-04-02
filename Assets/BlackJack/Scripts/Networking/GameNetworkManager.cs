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
    private void OnEnable()
    {
        NetworkCallbackManager.onJoinedRoom += OnJoinRoom;
        NetworkCallbackManager.onPlayerEnteredRoom += OnPlayerEnterRoom;
        NetworkCallbackManager.onPlayerLeftRoom += OnPlayerLeftRoom;
    }


    private void OnDisable()
    {
        NetworkCallbackManager.onJoinedRoom -= OnJoinRoom;
        NetworkCallbackManager.onPlayerEnteredRoom -= OnPlayerEnterRoom;
        NetworkCallbackManager.onPlayerLeftRoom -= OnPlayerLeftRoom;
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
}