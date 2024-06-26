using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileCanvas : CanvasBase
{
    [SerializeField] private AppDataSO appDataSo;
    [SerializeField] private TMP_Text nameInput;
    [SerializeField] private TMP_Text emailInput;
    [SerializeField] private TMP_Text mobileInput;
    [SerializeField] private Button editProfileButton;
    [SerializeField] private Button changePasswordButton;
    [SerializeField] private Button logoutButton;
    [SerializeField] private Button backButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase homeCanvas;

    [SerializeField] private CanvasBase editProfileCanvas;
    [SerializeField] private CanvasBase changePasswordCanvas;

    protected override void OnEnable()
    {
        base.OnEnable();
        SetData();
    }

    private void SetData()
    {
        nameInput.text = appDataSo.username;
        emailInput.text = appDataSo.email;
        mobileInput.text = appDataSo.mobile;
    }


    protected override void AddListener()
    {
        editProfileButton.onClick.AddListener(OnEditProfileButtonClick);
        changePasswordButton.onClick.AddListener(OnChangePasswordClick);
        logoutButton.onClick.AddListener(OnLogoutClick);
        backButton.onClick.AddListener(OnCancelClick);
    }

    protected override void RemoveListener()
    {
        editProfileButton.onClick.RemoveListener(OnEditProfileButtonClick);
        changePasswordButton.onClick.RemoveListener(OnChangePasswordClick);
        logoutButton.onClick.RemoveListener(OnLogoutClick);
        backButton.onClick.RemoveListener(OnCancelClick);
    }

    private void OnEditProfileButtonClick()
    {
        OnSetCanvasActive(editProfileCanvas);
    }

    private void OnCancelClick()
    {
        OnSetCanvasActive(homeCanvas);
    }


    private void OnChangePasswordClick()
    {
        OnSetCanvasActive(changePasswordCanvas);
    }

    private void OnLogoutClick()
    {
        var popContent = new PopContent("", "Are you sure you want\nto Logout ?");
        var buttonContentA = new ButtonContent("yes", OnClickYes);
        var buttonContentB = new ButtonContent("No", OnClickNo);
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
}