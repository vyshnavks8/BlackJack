using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavCanvas : CanvasBase
{
    [SerializeField] private RectTransform navParent;
    [SerializeField] private Button navCloseButton;
    [SerializeField] private Button profileButton;
    [SerializeField] private Button scoreboardButton;
    [SerializeField] private Button rulesButton;
    [SerializeField] private Button supportButton;
    [SerializeField] private Button volumeButton;
    [SerializeField] private Button aboutButton;
    [SerializeField] private Button termsButton;
    [SerializeField] private Button privacyButton;
    [SerializeField] private Button logoutButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase profileCanvas;

    [SerializeField] private CanvasBase volumeCanvas;
    [SerializeField] private CanvasBase supportCanvas;
    [SerializeField] private CanvasBase scoreboardCanvas;
    [SerializeField] private CanvasBase infoCanvas;
    [SerializeField] private CanvasBase gameCanvas;

    protected override void AddListener()
    {
        navCloseButton.onClick.AddListener(OnCloseNav);
        profileButton.onClick.AddListener(OnProfileClick);
        scoreboardButton.onClick.AddListener(OnScoreboardClick);
        rulesButton.onClick.AddListener(OnRulesClick);
        supportButton.onClick.AddListener(OnSupportClick);
        volumeButton.onClick.AddListener(OnVolumeClick);
        aboutButton.onClick.AddListener(OnAboutClick);
        termsButton.onClick.AddListener(OnTermsClick);
        privacyButton.onClick.AddListener(OnPrivacyClick);
        logoutButton.onClick.AddListener(OnLogoutClick);
    }


    protected override void RemoveListener()
    {
        navCloseButton.onClick.RemoveListener(OnCloseNav);
        profileButton.onClick.RemoveListener(OnProfileClick);
        scoreboardButton.onClick.RemoveListener(OnScoreboardClick);
        rulesButton.onClick.RemoveListener(OnRulesClick);
        supportButton.onClick.RemoveListener(OnSupportClick);
        volumeButton.onClick.RemoveListener(OnVolumeClick);
        aboutButton.onClick.RemoveListener(OnAboutClick);
        termsButton.onClick.RemoveListener(OnTermsClick);
        privacyButton.onClick.RemoveListener(OnPrivacyClick);
        logoutButton.onClick.RemoveListener(OnLogoutClick);
    }

   

    private void OnProfileClick()
    {
        profileCanvas.SetTransitionCanvas(transitionCanvas);
        OnSetCanvasActive(profileCanvas);
    }

    private void OnSupportClick()
    {
        supportCanvas.SetTransitionCanvas(transitionCanvas);
        OnSetCanvasActive(supportCanvas);
    }

    private void OnScoreboardClick()
    {
        scoreboardCanvas.SetTransitionCanvas(transitionCanvas);
        OnSetCanvasActive(scoreboardCanvas);
    }

    private void OnAboutClick()
    {
        InfoController.UpdateInfo("about", transitionCanvas);
        OnSetCanvasActive(infoCanvas);
    }

    private void OnRulesClick()
    {
        InfoController.UpdateInfo("rules", transitionCanvas);
        OnSetCanvasActive(infoCanvas);
    }

    private void OnVolumeClick()
    {
        volumeCanvas.SetTransitionCanvas(transitionCanvas);
        OnSetCanvasActive(volumeCanvas);
    }

    private void OnTermsClick()
    {
        InfoController.UpdateInfo("terms", transitionCanvas);
        OnSetCanvasActive(infoCanvas);
    }

    private void OnPrivacyClick()
    {
        InfoController.UpdateInfo("privacy", transitionCanvas);
        OnSetCanvasActive(infoCanvas);
    }

    private void OnLogoutClick()
    {
        var popContent = new PopContent("", "Are you sure you want\nto <size=90><b>Logout</size></b> ?");
        var buttonContentA = new ButtonContent("No", OnClickNo);
        var buttonContentB = new ButtonContent("Yes", OnClickYes);
        PopUpController.ShowPopUp(popContent, buttonContentA, buttonContentB);
    }

    private void OnClickYes()
    {
        PopUpController.ClosePopUp();
        BlackJackSave.ClearLoginData();
        SceneManager.LoadScene(SceneKey.Login);
    }

    private void OnClickNo()
    {
        PopUpController.ClosePopUp();
    }


    private void OnCloseNav()
    {
        OnSetCanvasOverlay(this, false);
    }
}