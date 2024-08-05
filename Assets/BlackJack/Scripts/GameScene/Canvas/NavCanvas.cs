using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavCanvas : CanvasBase
{
    [SerializeField] private RectTransform navParent;
    [SerializeField] private Button navCloseButton;
    [SerializeField] private Button profileButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button scoreboardButton;
    [SerializeField] private Button aboutButton;
    [SerializeField] private Button contactButton;
    [SerializeField] private Button logoutButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase profileCanvas;

    [SerializeField] private CanvasBase settingCanvas;
    [SerializeField] private CanvasBase scoreboardCanvas;
    [SerializeField] private CanvasBase infoCanvas;
    [SerializeField] private CanvasBase gameCanvas;

    protected override void AddListener()
    {
        navCloseButton.onClick.AddListener(OnCloseNav);
        profileButton.onClick.AddListener(OnProfileClick);
        settingsButton.onClick.AddListener(OnSettingsClick);
        scoreboardButton.onClick.AddListener(OnScoreboardClick);
        aboutButton.onClick.AddListener(OnAboutClick);
        contactButton.onClick.AddListener(OnContactClick);
        logoutButton.onClick.AddListener(OnLogoutClick);
    }


    protected override void RemoveListener()
    {
        navCloseButton.onClick.RemoveListener(OnCloseNav);
        profileButton.onClick.RemoveListener(OnProfileClick);
        settingsButton.onClick.RemoveListener(OnSettingsClick);
        scoreboardButton.onClick.RemoveListener(OnScoreboardClick);
        aboutButton.onClick.RemoveListener(OnAboutClick);
        contactButton.onClick.RemoveListener(OnContactClick);
        logoutButton.onClick.RemoveListener(OnLogoutClick);
    }


    private void OnProfileClick()
    {
        profileCanvas.SetTransitionCanvas(transitionCanvas);
        OnSetCanvasActive(profileCanvas);
    }

    private void OnSettingsClick()
    {
        settingCanvas.SetTransitionCanvas(transitionCanvas);
        OnSetCanvasActive(settingCanvas);
    }

    private void OnScoreboardClick()
    {
        scoreboardCanvas.SetTransitionCanvas(transitionCanvas);
        OnSetCanvasActive(scoreboardCanvas);
    }

    private void OnAboutClick()
    {
        InfoController.UpdateInfo("about",transitionCanvas);
        OnSetCanvasActive(infoCanvas);
    }

    private void OnContactClick()
    {
    }

    private void OnLogoutClick()
    {
        var popContent = new PopContent("", "Are you sure you want\nto Logout ?");
        var buttonContentA = new ButtonContent("No", OnClickNo);
        var buttonContentB = new ButtonContent("yes", OnClickYes);
        PopUpController.ShowPopUp(popContent, buttonContentA, buttonContentB);
    }

    private void OnClickYes()
    {
        PopUpController.ClosePopUp();
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