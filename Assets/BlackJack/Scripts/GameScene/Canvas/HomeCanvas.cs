using UnityEngine;
using UnityEngine.UI;

public class HomeCanvas : CanvasBase
{
    [SerializeField] private Button privateGameButton;
    [SerializeField] private Button publicGameButton;
    [SerializeField] private Button aiGameButton;
    [SerializeField] private Button navOpenButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase privateGameCanvas;

    [SerializeField] private CanvasBase gameCanvas;

    [SerializeField] private CanvasBase navMenuCanvas;

    protected override void OnEnable()
    {
        base.OnEnable();
        BlackJackApi.GetProfile();
    }

    private void OnOpenNav()
    {
        navMenuCanvas.SetTransitionCanvas(this);
        OnSetCanvasOverlay(navMenuCanvas, true);
    }

    protected override void AddListener()
    {
        privateGameButton.onClick.AddListener(OnPrivateGameClick);
        publicGameButton.onClick.AddListener(OnPublicGameClick);
        aiGameButton.onClick.AddListener(OnAiGameClick);
        navOpenButton.onClick.AddListener(OnOpenNav);
        NetworkCallbackManager.onConnectedToMaster += OnConnected;
    }


    protected override void RemoveListener()
    {
        privateGameButton.onClick.RemoveListener(OnPrivateGameClick);
        publicGameButton.onClick.RemoveListener(OnPublicGameClick);
        aiGameButton.onClick.RemoveListener(OnAiGameClick);
        navOpenButton.onClick.RemoveListener(OnOpenNav);
        NetworkCallbackManager.onConnectedToMaster -= OnConnected;
    }


    private void OnAiGameClick()
    {
        AppData.SetOnlineGameCode(null);
        AppData.SetGameType(GameType.AI);
        GameNetworkData.SetGameType(NetworkGameType.None);
        OnSetCanvasActive(gameCanvas);
    }

    private void OnPublicGameClick()
    {
        AppData.SetOnlineGameCode(null);
        AppData.SetGameType(GameType.Online);
        GameNetworkData.SetGameType(NetworkGameType.Random);
        NetworkManager.ConnectUsingSettings();
        LoadingController.ShowLoading();
    }

    private void OnPrivateGameClick()
    {
        AppData.SetGameType(GameType.Online);
        GameNetworkData.SetGameType(NetworkGameType.Friends);
        NetworkManager.ConnectUsingSettings();
        LoadingController.ShowLoading();
      
    }

    private void OnConnected()
    {
        LoadingController.HideLoading();
        GameNetworkData.SetPlayerData();
        switch (GameNetworkData.GetGameType)
        {
            case NetworkGameType.Random:
                OnSetCanvasActive(gameCanvas);
                break;
            case NetworkGameType.Friends:
                 OnSetCanvasActive(privateGameCanvas);
                break;
        }
       
    }
}