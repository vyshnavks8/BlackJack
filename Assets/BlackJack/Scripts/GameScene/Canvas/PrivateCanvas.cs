using UnityEngine;
using UnityEngine.UI;

public class PrivateCanvas : CanvasBase
{
    [SerializeField] private Button createButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button backButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase homeCanvas;

    [SerializeField] private CanvasBase gameCanvas;

    protected override void AddListener()
    {
        createButton.onClick.AddListener(OnCreateClick);
        joinButton.onClick.AddListener(OnJoinClick);
        backButton.onClick.AddListener(OnBackClick);
    }

    protected override void RemoveListener()
    {
        createButton.onClick.RemoveListener(OnCreateClick);
        joinButton.onClick.RemoveListener(OnJoinClick);
        backButton.onClick.RemoveListener(OnBackClick);
    }

    private void OnBackClick()
    {
        OnSetCanvasActive(homeCanvas);
    }

    private void OnJoinClick()
    {
        OnSetCanvasActive(gameCanvas);
    }

    private void OnCreateClick()
    {
        OnSetCanvasActive(gameCanvas);
    }
}