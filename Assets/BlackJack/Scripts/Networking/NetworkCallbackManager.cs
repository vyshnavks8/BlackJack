using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkCallbackManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private bool showDebugs;
    private static NetworkCallbackManager instance;
    public static event Action onConnectedToMaster;
    public static event Action<DisconnectCause> onDisconnected;
    public static event Action<Player> onMasterClientSwitched;
    public static event Action onCreatedRoom;
    public static event Action<short, string> onCreateRoomFailed;
    public static event Action onJoinedRoom;
    public static event Action<short, string> onJoinRoomFailed;
    public static event Action<short, string> onJoinRandomFailed;
    public static event Action onLeftRoom;
    public static event Action onJoinedLobby;
    public static event Action onLeftLobby;
    public static event Action<Player> onPlayerEnteredRoom;
    public static event Action<Player> onPlayerLeftRoom;
    public static event Action<EventData> onEventReceived;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.NetworkingClient.EventReceived += HandleRaiseEvents;
    }


    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.NetworkingClient.EventReceived -= HandleRaiseEvents;
    }

    private void HandleRaiseEvents(EventData eventData)
    {
        onEventReceived?.Invoke(eventData);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        onConnectedToMaster?.Invoke();
        if (showDebugs) Debug.Log("ConnectedToMaster");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        onDisconnected?.Invoke(cause);
        if (showDebugs) Debug.Log("Disconnected");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
        onMasterClientSwitched?.Invoke(newMasterClient);
        if (showDebugs) Debug.Log("MasterClientSwitched ActorID:"+ newMasterClient.ActorNumber);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        onCreatedRoom?.Invoke();
        if (showDebugs) Debug.Log("CreatedRoom");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        onCreateRoomFailed?.Invoke(returnCode, message);
        if (showDebugs) Debug.Log("CreateRoomFailed :"+message);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        onJoinedRoom?.Invoke();
        if (showDebugs) Debug.Log("JoinedRoom");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        onJoinRoomFailed?.Invoke(returnCode, message);
        if (showDebugs) Debug.Log("JoinRoomFailed :"+message);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        onJoinRandomFailed?.Invoke(returnCode, message);
        if (showDebugs) Debug.Log("JoinRandomFailed :"+message);
    }


    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        onLeftRoom?.Invoke();
        if (showDebugs) Debug.Log("LeftRoom");
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        onJoinedLobby?.Invoke();
        if (showDebugs) Debug.Log("JoinedLobby");
    }

    public override void OnLeftLobby()
    {
        base.OnLeftLobby();
        onLeftLobby?.Invoke();
        if (showDebugs) Debug.Log("LeftLobby");
    }

    public override void OnPlayerEnteredRoom(Player player)
    {
        base.OnPlayerEnteredRoom(player);
        onPlayerEnteredRoom?.Invoke(player);
        if (showDebugs) Debug.Log("PlayerEnteredRoom ActorID:" + player.ActorNumber);
    }

    public override void OnPlayerLeftRoom(Player player)
    {
        base.OnPlayerLeftRoom(player);
        onPlayerLeftRoom?.Invoke(player);
        if (showDebugs) Debug.Log("PlayerLeftRoom ActorID:" + player.ActorNumber);
    }
}