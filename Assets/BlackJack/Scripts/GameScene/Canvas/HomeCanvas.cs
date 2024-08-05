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

    [SerializeField]  private CanvasBase navMenuCanvas;


    protected override void AddListener()
    {
        privateGameButton.onClick.AddListener(OnPrivateGameClick);
        publicGameButton.onClick.AddListener(OnPublicGameClick);
        aiGameButton.onClick.AddListener(OnAiGameClick);

        navOpenButton.onClick.AddListener(OnOpenNav);
    }

    private void OnOpenNav()
    {
        navMenuCanvas.SetTransitionCanvas(this);
        OnSetCanvasOverlay(navMenuCanvas,true);
    }


    protected override void RemoveListener()
    {
        privateGameButton.onClick.RemoveListener(OnPrivateGameClick);
        publicGameButton.onClick.RemoveListener(OnPublicGameClick);
        aiGameButton.onClick.RemoveListener(OnAiGameClick);
        navOpenButton.onClick.RemoveListener(OnOpenNav);
    }


    private void OnAiGameClick()
    {
        OnSetCanvasActive(privateGameCanvas);
    }

    private void OnPublicGameClick()
    {
        OnSetCanvasActive(privateGameCanvas);
    }

    private void OnPrivateGameClick()
    {
        OnSetCanvasActive(privateGameCanvas);
    }
}