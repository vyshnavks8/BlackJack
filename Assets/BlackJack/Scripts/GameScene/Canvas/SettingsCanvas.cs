using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsCanvas : CanvasBase
{
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle effectToggle;
    [SerializeField] private Button termsConditionButton;
    [SerializeField] private Button privacyButton;
    [SerializeField] private Button backButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase homeCanvas;
    private bool music;
    private bool effect;
    protected override void AddListener()
    {
        termsConditionButton.onClick.AddListener(OnTermsClick);
        privacyButton.onClick.AddListener(OnPrivacyClick);
        backButton.onClick.AddListener(OnBackClick);
        musicToggle.onValueChanged.AddListener(OnMusicToggle);
        effectToggle.onValueChanged.AddListener(OnEffectToggle);
    }

    protected override void RemoveListener()
    {
        termsConditionButton.onClick.RemoveListener(OnTermsClick);
        privacyButton.onClick.RemoveListener(OnPrivacyClick);
        backButton.onClick.RemoveListener(OnBackClick);
        musicToggle.onValueChanged.RemoveListener(OnMusicToggle);
        effectToggle.onValueChanged.RemoveListener(OnEffectToggle);
    }

 

    private void OnEffectToggle(bool value)
    {
        effect = value;
    }

    private void OnMusicToggle(bool value)
    {
        music = value;
    }


    private void OnBackClick()
    {
        OnSetCanvasActive(homeCanvas);
    }

    private void OnTermsClick()
    {
        var popContent = new PopContent("Terms & Condition", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
        var buttonContentA = new ButtonContent("OK", OnClickOk);
        PopUpController.ShowPopUp(popContent, buttonContentA);
    }
    private void OnPrivacyClick()
    {
        var popContent = new PopContent("Privacy Policy", "    Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.");
        var buttonContentA = new ButtonContent("OK", OnClickOk);
        PopUpController.ShowPopUp(popContent, buttonContentA);
    }

    private void OnClickOk()
    {
        PopUpController.ClosePopUp();
    }
}