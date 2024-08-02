using UnityEngine;
using UnityEngine.UI;

public class SettingsCanvas : CanvasBase
{
    [SerializeField] private Button volumeButton;
    [SerializeField] private Button termsConditionButton;
    [SerializeField] private Button privacyButton;
    [SerializeField] private Button backButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase homeCanvas;[SerializeField]
    private CanvasBase volumeCanvas;

    [SerializeField] private CanvasBase infoCanvas;

    protected override void AddListener()
    {
        volumeButton.onClick.AddListener(OnVolumeClick);
        termsConditionButton.onClick.AddListener(OnTermsClick);
        privacyButton.onClick.AddListener(OnPrivacyClick);
        backButton.onClick.AddListener(OnBackClick);
    }

    protected override void RemoveListener()
    {
        volumeButton.onClick.RemoveListener(OnVolumeClick);
        termsConditionButton.onClick.RemoveListener(OnTermsClick);
        privacyButton.onClick.RemoveListener(OnPrivacyClick);
        backButton.onClick.RemoveListener(OnBackClick);
    }

  
    private void OnBackClick()
    {
        OnSetCanvasActive(homeCanvas);
    }
    private void OnVolumeClick()
    {
        OnSetCanvasActive(volumeCanvas);
    }


    private void OnTermsClick()
    {
        InfoController.UpdateInfo("terms", this);
        OnSetCanvasActive(infoCanvas);
    }

    private void OnPrivacyClick()
    {
        InfoController.UpdateInfo("privacy", this);
        OnSetCanvasActive(infoCanvas);
    }
}