using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinCanvas : CanvasBase
{
    [SerializeField] private TMP_InputField roomInput;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button backButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase privateCanvas;

    [SerializeField] private CanvasBase gameCanvas;
    private string gameCode;

    protected override void AddListener()
    {
        roomInput.onValueChanged.AddListener(OnRoomInput);
        joinButton.onClick.AddListener(OnJoinClick);
        backButton.onClick.AddListener(OnBackClick);
    }

    protected override void RemoveListener()
    {
        roomInput.onValueChanged.RemoveListener(OnRoomInput);
        joinButton.onClick.RemoveListener(OnJoinClick);
        backButton.onClick.RemoveListener(OnBackClick);
    }

    private void OnRoomInput(string value)
    {
        gameCode = value;
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
        APIHandler.Get<JoinGameResponse>(ApiUrl.JoinGame + gameCode, null, OnCreateGameCallback);
        LoadingController.ShowLoading();
    }
    private void OnCreateGameCallback(bool success, JoinGameResponse response)
    {
        LoadingController.HideLoading();
        if (success)
        {
            if (response.success)
            {
                AppData.SetOnlineGameCode(gameCode);
                OnSetCanvasActive(gameCanvas);
            }
            else
            {
                NetworkPopUp.ShowPopUp("Join Private Game", response.message);
            }
          
        }
        else
        {
            NetworkPopUp.ShowPopUp("Join Private Game", response.message);
        }
    }

    private bool CheckValidInputs()
    {
        if (BlackjackUtils.IsInputEmpty(gameCode, "Game Code")) return false;
        return true;
    }

  
    protected override void Close()
    {
       roomInput.text = null;
    }
}