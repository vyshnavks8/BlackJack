using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateCanvas : CanvasBase
{
    [SerializeField] private TMP_InputField gameCodeInput;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button backButton;

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
    }


    protected override void RemoveListener()
    {
        gameCodeInput.onValueChanged.RemoveListener(OnGameCodeSet);
        joinButton.onClick.RemoveListener(OnJoinClick);
        backButton.onClick.RemoveListener(OnBackClick);
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

        if (!CheckValidInputs()) return;
        var createGameData = new CreateGameData
        {
            gameDetails = gameCode,
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