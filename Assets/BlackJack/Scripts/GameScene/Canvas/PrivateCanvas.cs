using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PrivateCanvas : CanvasBase
{
    [SerializeField] private Button createButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button backButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase homeCanvas;
    [SerializeField] private CanvasBase createCanvas;
    [SerializeField] private CanvasBase joinCanvas;

    protected override void AddListener()
    {
        createButton.onClick.AddListener(OnCreateClick);
        joinButton.onClick.AddListener(OnJoinClick);
        backButton.onClick.AddListener(OnBackClick);
        NetworkCallbackManager.onDisconnected += OnDisconnect;
    }

    protected override void RemoveListener()
    {
        createButton.onClick.RemoveListener(OnCreateClick);
        joinButton.onClick.RemoveListener(OnJoinClick);
        backButton.onClick.RemoveListener(OnBackClick);
        NetworkCallbackManager.onDisconnected -= OnDisconnect;
    }
   
    private void OnBackClick()
    { 
        LoadingController.ShowLoading();
        GameNetworkData.SetPrivateGameType(PrivateGameType.None);
        NetworkManager.Disconnect();
    }

    private void OnJoinClick()
    {
        GameNetworkData.SetPrivateGameType(PrivateGameType.Join);
        OnSetCanvasActive(joinCanvas);
    }

    private void OnCreateClick()
    {
        GameNetworkData.SetPrivateGameType(PrivateGameType.Create);
        OnSetCanvasActive(createCanvas);
    }
    private void OnDisconnect(DisconnectCause cause)
    {
        LoadingController.HideLoading();
        switch (cause)
        {
            case DisconnectCause.DisconnectByClientLogic:
                OnSetCanvasActive(homeCanvas);
                break;
        }
    }

}