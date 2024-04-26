using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : CanvasBase
{
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
        OnSetCanvasActive(homeCanvas);
    }

    private void OnCardsClick()
    {
        myCards.SetActive(true);
    }

    private void OnCloseCardsClick()
    {
        myCards.SetActive(false);
    }

    private void OnScoreboardClick()
    {
        OnSetCanvasActive(scoreboardCanvas);
    }

    private void OnChatClick()
    {
        OnSetCanvasActive(chatCanvas);
    }
}