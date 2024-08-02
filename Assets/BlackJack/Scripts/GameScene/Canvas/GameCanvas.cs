using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : CanvasBase
{
    [SerializeField] private float transitionDuration = 0.3f;
    [SerializeField] private GameObject myCards;
    [SerializeField] private Button chatButton;
    [SerializeField] private Button cardsButton;
    [SerializeField] private Button closeCardsButton;
    [SerializeField] private Button scoreboardButton;
    [SerializeField] private Button backButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase homeCanvas;

    [SerializeField] private CanvasBase chatCanvas;
    [SerializeField] private CanvasBase scoreboardCanvas;

    protected override void AddListener()
    {
        chatButton.onClick.AddListener(OnChatClick);
        scoreboardButton.onClick.AddListener(OnScoreboardClick);
        cardsButton.onClick.AddListener(OnCardsClick);
        closeCardsButton.onClick.AddListener(OnCloseCardsClick);
        backButton.onClick.AddListener(OnBackClick);
    }

    protected override void RemoveListener()
    {
        chatButton.onClick.RemoveListener(OnChatClick);
        scoreboardButton.onClick.RemoveListener(OnScoreboardClick);
        cardsButton.onClick.RemoveListener(OnCardsClick);
        closeCardsButton.onClick.RemoveListener(OnCloseCardsClick);
        backButton.onClick.RemoveListener(OnBackClick);
    }


    private void OnBackClick()
    {
        var popContent = new PopContent("", "Are you sure you want\nto Exit Game ?");
        var buttonContentA = new ButtonContent("No", OnClickNo);
        var buttonContentB = new ButtonContent("yes", OnClickYes);
        PopUpController.ShowPopUp(popContent, buttonContentA, buttonContentB);
    }
    private void OnClickYes()
    {
        PopUpController.ClosePopUp();
        OnSetCanvasActive(homeCanvas);
    }

    private void OnClickNo()
    {
        PopUpController.ClosePopUp();
    }
    private void OnCardsClick()
    {
        myCards.transform.DOLocalMoveY(-4000, 0).OnComplete(() =>
        {
            myCards.SetActive(true);
            myCards.transform.DOLocalMoveY(0, transitionDuration).SetEase(Ease.OutQuad);
        });
       
    }

    private void OnCloseCardsClick()
    {
        myCards.transform.DOLocalMoveY(-4000, transitionDuration).SetEase(Ease.InQuad).OnComplete(() =>
        {
            myCards.SetActive(false);
        });
     
    }

    private void OnScoreboardClick()
    {
        scoreboardCanvas.SetTransitionCanvas(this);
        OnSetCanvasActive(scoreboardCanvas);
    }

    private void OnChatClick()
    {
        OnSetCanvasActive(chatCanvas);
    }
}