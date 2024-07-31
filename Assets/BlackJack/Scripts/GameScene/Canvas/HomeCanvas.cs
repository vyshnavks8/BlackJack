using UnityEngine;
using UnityEngine.UI;

public class HomeCanvas : CanvasBase
{
    [SerializeField] private Button profileButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button privateGameButton;
    [SerializeField] private Button publicGameButton;
    [SerializeField] private Button aiGameButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase privateGameCanvas;

    [SerializeField] private CanvasBase profileCanvas;
    [SerializeField] private CanvasBase settingCanvas;
    [SerializeField] private CanvasBase gameCanvas;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

 

    protected override void AddListener()
    {
        profileButton.onClick.AddListener(OnProfileClick);
        settingsButton.onClick.AddListener(OnSettingsClick);
        privateGameButton.onClick.AddListener(OnPrivateGameClick);
        publicGameButton.onClick.AddListener(OnPublicGameClick);
        aiGameButton.onClick.AddListener(OnAiGameClick);
    }

 
    protected override void RemoveListener()
    {
        profileButton.onClick.RemoveListener(OnProfileClick);
        settingsButton.onClick.RemoveListener(OnSettingsClick);
        privateGameButton.onClick.RemoveListener(OnPrivateGameClick);
        publicGameButton.onClick.RemoveListener(OnPublicGameClick);
        aiGameButton.onClick.RemoveListener(OnAiGameClick);
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

    private void OnProfileClick()
    {
        OnSetCanvasActive(profileCanvas);
    }
    private void OnSettingsClick()
    {
        OnSetCanvasActive(settingCanvas);
    }

}