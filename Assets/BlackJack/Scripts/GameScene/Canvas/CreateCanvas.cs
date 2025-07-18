using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateCanvas : CanvasBase
{
    [SerializeField] private TMP_InputField gameCodeInput;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button dealerTimeInfoButton;
    [SerializeField] private Button playerTimeInfoButton;
    [SerializeField] private Button noPlayerInfoButton;


    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase privateCanvas;

    [SerializeField] private CanvasBase gameCanvas;
    private string generatedCode;
    private const byte maxPlayers = 5;

    protected override void AddListener()
    {
        gameCodeInput.onValueChanged.AddListener(OnGameCodeSet);
        joinButton.onClick.AddListener(OnJoinClick);
        backButton.onClick.AddListener(OnBackClick);

        dealerTimeInfoButton.onClick.AddListener(OnDealerTimeInfoClick);
        playerTimeInfoButton.onClick.AddListener(OnPlayerTimeInfoClick);
        noPlayerInfoButton.onClick.AddListener(OnNoPlayerInfoClick);
        NetworkCallbackManager.onCreatedRoom += OnCreateRoom;
        NetworkCallbackManager.onCreateRoomFailed += OnCreateRoomFailed;
    }


    protected override void RemoveListener()
    {
        gameCodeInput.onValueChanged.RemoveListener(OnGameCodeSet);
        joinButton.onClick.RemoveListener(OnJoinClick);
        backButton.onClick.RemoveListener(OnBackClick);

        dealerTimeInfoButton.onClick.RemoveListener(OnDealerTimeInfoClick);
        playerTimeInfoButton.onClick.RemoveListener(OnPlayerTimeInfoClick);
        noPlayerInfoButton.onClick.RemoveListener(OnNoPlayerInfoClick);
        NetworkCallbackManager.onCreatedRoom -= OnCreateRoom;
        NetworkCallbackManager.onCreateRoomFailed -= OnCreateRoomFailed;
    }

    private void OnNoPlayerInfoClick()
    {
        NetworkPopUp.ShowPopUp(null, "No of player message");
    }

    private void OnPlayerTimeInfoClick()
    {
        NetworkPopUp.ShowPopUp(null, "Player Decision Timer message");
    }

    private void OnDealerTimeInfoClick()
    {
        NetworkPopUp.ShowPopUp(null, "Dealer Declare Times Around the Table message");
    }

    private void OnGameCodeSet(string input)
    {
        // gameCode = input;
    }

    private void OnBackClick()
    {
        OnSetCanvasActive(privateCanvas);
    }

    private void OnJoinClick()
    {
        if (skipApiCall)
        {
            LoadingController.ShowLoading();
            NetworkManager.CreateRoom("a", maxPlayers);
            AppData.SetOnlineGameCode("a");
            return;
        }

        var createGameData = new CreateGameData
        {
            gameDetails = AppData.username,
        };
        APIHandler.Post<CreateGameResponse>(ApiUrl.CreateGame, createGameData, OnCreateGameCallback);
        LoadingController.ShowLoading();
    }

    private void OnCreateGameCallback(bool success, CreateGameResponse response)
    {
        LoadingController.HideLoading();
        if (success)
        {
            if (response.success)
            {
                LoadingController.ShowLoading();
                NetworkManager.CreateRoom(response.gameCode, maxPlayers);
                AppData.SetOnlineGameCode(response.gameCode, response.message);
            }
            else
            {
                NetworkPopUp.ShowPopUp("Create Private Game", response.message);
            }
        }
        else
        {
            NetworkPopUp.ShowPopUp("Create Private Game", response.message);
        }
    }

    private void OnCreateRoom()
    {
        LoadingController.HideLoading();
        OnSetCanvasActive(gameCanvas);
        var shareButton = new ButtonContent("Share Code", OnClickShareCode);
        NetworkPopUp.ShowPopUp("Create Private Game", AppData.onlineGameMessage + "\n" + "JOIN GAME CODE : " + AppData.onlineGameCode, shareButton);
    }

    private void OnCreateRoomFailed(short arg1, string message)
    {
        LoadingController.HideLoading();
        NetworkPopUp.ShowPopUp("Create Private Game", message);
    }

    private void OnClickShareCode()
    {
        BlackjackUtils.ShareOnlineGameCode();
    }
}