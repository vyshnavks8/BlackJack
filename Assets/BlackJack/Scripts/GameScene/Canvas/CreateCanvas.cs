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
    private string gameCode;
    private string generatedCode;

    protected override void AddListener()
    {
        gameCodeInput.onValueChanged.AddListener(OnGameCodeSet);
        joinButton.onClick.AddListener(OnJoinClick);
        backButton.onClick.AddListener(OnBackClick);
        
        dealerTimeInfoButton.onClick.AddListener(OnDealerTimeInfoClick);
        playerTimeInfoButton.onClick.AddListener(OnPlayerTimeInfoClick);
        noPlayerInfoButton.onClick.AddListener(OnNoPlayerInfoClick);
    }

    private void OnNoPlayerInfoClick()
    {
       NetworkPopUp.ShowPopUp(null,"No of player message");
    }

    private void OnPlayerTimeInfoClick()
    {
        NetworkPopUp.ShowPopUp(null,"Player Decision Timer message");
    }

    private void OnDealerTimeInfoClick()
    {
        NetworkPopUp.ShowPopUp(null,"Dealer Declare Times Around the Table message");
    }


    protected override void RemoveListener()
    {
        gameCodeInput.onValueChanged.RemoveListener(OnGameCodeSet);
        joinButton.onClick.RemoveListener(OnJoinClick);
        backButton.onClick.RemoveListener(OnBackClick);
        
        dealerTimeInfoButton.onClick.RemoveListener(OnDealerTimeInfoClick);
        playerTimeInfoButton.onClick.RemoveListener(OnPlayerTimeInfoClick);
        noPlayerInfoButton.onClick.RemoveListener(OnNoPlayerInfoClick);
    }

    private void OnGameCodeSet(string input)
    {
        gameCode = input;
    }

    private void OnBackClick()
    {
        OnSetCanvasActive(privateCanvas);
    }

    private void OnJoinClick()
    {
        if (skipApiCall)
        {
            OnSetCanvasActive(gameCanvas);
            return;
        }

        //if (!CheckValidInputs()) return;
        var createGameData = new CreateGameData
        {
            gameDetails = AppData.username,
        };
        APIHandler.Post<CreateGameResponse>(ApiUrl.CreateGame, createGameData, OnCreateGameCallback);
    }

    private bool CheckValidInputs()
    {
        if (BlackjackUtils.IsInputEmpty(gameCode, "Game Code")) return false;
        return true;
    }

    private void OnCreateGameCallback(bool success, CreateGameResponse response)
    {
        if (success)
        {
            OnSetCanvasActive(gameCanvas);
            AppData.SetOnlineGameCode(response.gameCode);
            var shareButton = new ButtonContent("Share Code", OnClickShareCode);
            NetworkPopUp.ShowPopUp("Create Private Game",
                response.message + "\n" + "JOIN GAME CODE : " + response.gameCode, shareButton);
        }
        else
        {
            NetworkPopUp.ShowPopUp("Create Private Game", response.message);
        }
    }

    private void OnClickShareCode()
    {
        BlackjackUtils.ShareOnlineGameCode();
    }
}