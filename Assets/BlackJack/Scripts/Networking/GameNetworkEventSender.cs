using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Newtonsoft.Json;
using Photon.Realtime;
using RedDevil.PlayingCards;
using UnityEngine;

public class GameNetworkEventSender : MonoBehaviour
{
    [SerializeField] GameNetworkManager gameNetworkManager;
    public event Action<int> OnPlaceChipAmount;
    public event Action<PlayerChoice> OnPlayerChoice;
    public event Action<Deck> OnInitDeck;
    public event Action<int> OnPlayerLeft;
    public event Action<int, string> OnChatReceived;


    private void OnEnable()
    {
        NetworkCallbackManager.onEventReceived += OnEventReceived;
        gameNetworkManager.networkPlayersCanvas.OnStartButtonPressed += StartGame;
        gameNetworkManager.SyncPlayerList += SyncPlayerList;
        gameNetworkManager.PlayerLeft += PlayerLeftGame;
    }


    private void OnDisable()
    {
        NetworkCallbackManager.onEventReceived -= OnEventReceived;
        gameNetworkManager.networkPlayersCanvas.OnStartButtonPressed -= StartGame;
        gameNetworkManager.SyncPlayerList -= SyncPlayerList;
        gameNetworkManager.PlayerLeft -= PlayerLeftGame;
    }

    private void OnEventReceived(EventData eventData)
    {
        switch (eventData.Code)
        {
            case NetworkEventCode.SyncPlayerList:
                SyncPlayerListEvent(eventData.CustomData);
                break;
            case NetworkEventCode.StartGame:
                StartGameEvent(eventData.CustomData);
                break;
            case NetworkEventCode.PlaceBet:
                PlacePlayerChipEvent(eventData.CustomData);
                break;
            case NetworkEventCode.InitDeck:
                InitDeckEvent(eventData.CustomData);
                break;
            case NetworkEventCode.PlaceChoice:
                PlacePlayerChoiceEvent(eventData.CustomData);
                break;
            case NetworkEventCode.Chat:
                SendChatEvent(eventData.CustomData);
                break;
        }
    }

    public void SendChat(int id, string message)
    {
        var hashtable = new Hashtable
        {
            { 0, id },
            { 1, message }
        };
        NetworkManager.RaiseEvent(hashtable, NetworkEventCode.Chat, ReceiverGroup.All);
    }

    private void SendChatEvent(object eventData)
    {
        var dataTable = (Hashtable)eventData;
        var id = (int)dataTable[0];
        var message = (string)dataTable[1];
        OnChatReceived?.Invoke(id, message);
    }

    private void PlayerLeftGame(Player obj)
    {
        OnPlayerLeft?.Invoke(obj.ActorNumber);
    }

    private void SyncPlayerList()
    {
        var data = JsonConvert.SerializeObject(gameNetworkManager.playersList);
        var hashtable = new Hashtable
        {
            { 0, data },
        };
        NetworkManager.RaiseEvent(hashtable, NetworkEventCode.SyncPlayerList, ReceiverGroup.Others);
    }

    private void SyncPlayerListEvent(object eventData)
    {
        var dataTable = (Hashtable)eventData;
        var data = (string)dataTable[0];
        var idList = JsonConvert.DeserializeObject<List<int>>(data);
        gameNetworkManager.playersList.Clear();
        gameNetworkManager.networkPlayersCanvas.ClearPlayers();
        gameNetworkManager.playersList.AddRange(idList);
        GameNetworkData.ClearAllIcons();
        foreach (var id in idList)
        {
            var player = NetworkManager.CurrentRoom.GetPlayer(id);
            var playerData = GameNetworkData.GetPlayerData(player);
            gameNetworkManager.networkPlayersCanvas.AddPlayer(player.ActorNumber, playerData);
            GameNetworkData.AddPlayerIcon(id, playerData.playerIcon);
        }
    }

    private void StartGame()
    {
        if (!NetworkManager.IsMasterClient) return;
        NetworkManager.CurrentRoom.IsOpen = false;
        var hashtable = new Hashtable
        {
            { 0, gameNetworkManager.playersList.Count },
        };
        NetworkManager.RaiseEvent(hashtable, NetworkEventCode.StartGame, ReceiverGroup.All);
    }

    private void StartGameEvent(object eventData)
    {
        var dataTable = (Hashtable)eventData;
        var playerCount = (int)dataTable[0];
        gameNetworkManager.networkPlayersCanvas.HideCanvas();
        gameNetworkManager.blackJackManager.SetNetworkPlayer(playerCount, gameNetworkManager.playersList);
        gameNetworkManager.blackJackManager.StartGame();
    }

    public void PlacePlayerChip(int amount)
    {
        var hashtable = new Hashtable
        {
            { 0, amount },
        };
        NetworkManager.RaiseEvent(hashtable, NetworkEventCode.PlaceBet, ReceiverGroup.All);
    }


    private void PlacePlayerChipEvent(object eventData)
    {
        var dataTable = (Hashtable)eventData;
        var chipAmount = (int)dataTable[0];
        OnPlaceChipAmount?.Invoke(chipAmount);
    }

    public void PlacePlayerChoice(PlayerChoice choice)
    {
        var hashtable = new Hashtable
        {
            { 0, choice },
        };
        NetworkManager.RaiseEvent(hashtable, NetworkEventCode.PlaceChoice, ReceiverGroup.All);
    }

    private void PlacePlayerChoiceEvent(object eventData)
    {
        var dataTable = (Hashtable)eventData;
        var playerChoice = (PlayerChoice)dataTable[0];
        OnPlayerChoice?.Invoke(playerChoice);
    }

    public void InitDeck(Deck deck)
    {
        var data = JsonConvert.SerializeObject(deck);
        var hashtable = new Hashtable
        {
            { 0, data },
        };
        NetworkManager.RaiseEvent(hashtable, NetworkEventCode.InitDeck, ReceiverGroup.All);
    }

    private void InitDeckEvent(object eventData)
    {
        var dataTable = (Hashtable)eventData;
        var data = (string)dataTable[0];
        var deck = JsonConvert.DeserializeObject<Deck>(data);
        OnInitDeck?.Invoke(deck);
    }
}