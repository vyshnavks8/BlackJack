using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : CanvasBase
{
    [SerializeField] private TMP_Text codeText;
    [SerializeField] private Button backButton;
    [SerializeField] private Button navOpenButton;
   
    [SerializeField] private Button shareButton;
    [Header("Cards")] [SerializeField] private Button cardsButton;

    [SerializeField] private OverlayCanvas cardCanvas;

    [Header("Scoreboard")] [SerializeField]
    private Button scoreboardButton;

    [SerializeField] private OverlayCanvas scoreboardCanvas;
    [Header("Chat")]
    [SerializeField] private Button chatButton;
    [SerializeField] private OverlayCanvas chatCanvas;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase homeCanvas;
    [SerializeField] private CanvasBase infoCanvas;
    [SerializeField] private CanvasBase navMenuCanvas;
    public event Action OnStartGame;
    public event Action OnExitGame;
    

    public override void Ready()
    {
        base.Ready();
        SetGameCode();
        if (GameNetworkData.GetGameType == NetworkGameType.None)
        {
            OnStartGame?.Invoke();
        }
    }

    private void SetGameCode()
    {
        if (!string.IsNullOrEmpty(AppData.onlineGameCode))
        {
            codeText.text = "GAME CODE : " + AppData.onlineGameCode;
        }
        else
        {
            codeText.text = "";
        }
    }

    protected override void AddListener()
    {
        chatButton.onClick.AddListener(OnChatClick);
        scoreboardButton.onClick.AddListener(OnScoreboardClick);
        cardsButton.onClick.AddListener(OnCardsClick);
        backButton.onClick.AddListener(OnBackClick);
        navOpenButton.onClick.AddListener(OnOpenNav);
        shareButton.onClick.AddListener(OnShareClick);
    }

    protected override void RemoveListener()
    {
        chatButton.onClick.RemoveListener(OnChatClick);
        scoreboardButton.onClick.RemoveListener(OnScoreboardClick);
        cardsButton.onClick.RemoveListener(OnCardsClick);
        backButton.onClick.RemoveListener(OnBackClick);
        navOpenButton.onClick.RemoveListener(OnOpenNav);
        shareButton.onClick.RemoveListener(OnShareClick);
    }

    private void OnShareClick()
    {
        BlackjackUtils.ShareOnlineGameCode();
    }

    private void OnOpenNav()
    {
        navMenuCanvas.SetTransitionCanvas(this);
        OnSetCanvasOverlay(navMenuCanvas, true);
    }

    private void OnBackClick()
    {
        var popContent = new PopContent("", "Are You Sure You Want\nTo <size=150><b>EXIT</size></b> Game ?");
        var buttonContentA = new ButtonContent("No", OnClickNo);
        var buttonContentB = new ButtonContent("Yes", OnClickYes);
        PopUpController.ShowPopUp(popContent, buttonContentA, buttonContentB);
    }

    private void OnClickYes()
    {
        GotoHome();
    }

    public void GotoHome()
    {
        if (GameNetworkData.GetGameType != NetworkGameType.None)
        {
            NetworkManager.Disconnect();
        }
        PopUpController.ClosePopUp();
        OnSetCanvasActive(homeCanvas);
        OnExitGame?.Invoke();
    }

    private void OnClickNo()
    {
        PopUpController.ClosePopUp();
    }

    private void OnCardsClick()
    {
        cardCanvas.ShowOverlay();
    }

    private void OnScoreboardClick()
    {
        scoreboardCanvas.ShowOverlay();
    }
    private void OnChatClick()
    {
        chatCanvas.ShowOverlay();
    }

    public void SetButtonActive(bool active)
    {
        navOpenButton.interactable = active;
        chatButton.interactable = active;
    }
  
    public void HideChatOption()
    {
        chatButton.gameObject.SetActive(false);
    }
}