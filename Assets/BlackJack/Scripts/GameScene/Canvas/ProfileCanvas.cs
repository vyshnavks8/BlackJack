using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileCanvas : CanvasBase
{
    [SerializeField] private TMP_Text nameInput;
    [SerializeField] private TMP_Text emailInput;
    [SerializeField] private TMP_Text mobileInput;
    [SerializeField] private Button editProfileButton;
    [SerializeField] private Button changePasswordButton;
    [SerializeField] private Button deleteAccountButton;
    [SerializeField] private Button backButton;

    [Header("Profile Image")] [SerializeField]
    private TMP_Text profileImagText;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase editProfileCanvas;

    [SerializeField] private CanvasBase changePasswordCanvas;

    protected override void OnEnable()
    {
        base.OnEnable();
        BlackJackApi.GetProfile();
        SetData();
    }

    private void SetData()
    {
        nameInput.text = !string.IsNullOrEmpty(AppData.username) ? AppData.username : "New User";
        emailInput.text = AppData.email;
        mobileInput.text = AppData.mobile;
        profileImagText.text = AppData.username[0].ToString();
    }


    protected override void AddListener()
    {
        editProfileButton.onClick.AddListener(OnEditProfileButtonClick);
        changePasswordButton.onClick.AddListener(OnChangePasswordClick);
        deleteAccountButton.onClick.AddListener(OnDeleteAccountClick);
        backButton.onClick.AddListener(OnCancelClick);
        AppData.OnUpdateUserData += SetData;
    }

    protected override void RemoveListener()
    {
        editProfileButton.onClick.RemoveListener(OnEditProfileButtonClick);
        changePasswordButton.onClick.RemoveListener(OnChangePasswordClick);
        deleteAccountButton.onClick.RemoveListener(OnDeleteAccountClick);
        backButton.onClick.RemoveListener(OnCancelClick);
        AppData.OnUpdateUserData -= SetData;
    }

    private void OnEditProfileButtonClick()
    {
        OnSetCanvasActive(editProfileCanvas);
    }

    private void OnCancelClick()
    {
        OnSetCanvasActive(transitionCanvas);
    }


    private void OnChangePasswordClick()
    {
        OnSetCanvasActive(changePasswordCanvas);
    }

    private void OnDeleteAccountClick()
    {
        var popContent = new PopContent("", "Are You Sure You Want\nTo <size=150><b>DELETE</b></size> Account ?");
        var buttonContentA = new ButtonContent("No", OnClickNo);
        var buttonContentB = new ButtonContent("Yes", OnClickYes);
        PopUpController.ShowPopUp(popContent, buttonContentA, buttonContentB);
    }

    private void OnClickYes()
    {
        PopUpController.ClosePopUp();
        APIHandler.Delete<DeleteProfileResponse>(ApiUrl.DeleteProfile, null, OnDeleteAccountCallback);
    }

    private void OnDeleteAccountCallback(bool success, DeleteProfileResponse response)
    {
        if (response.success)
        {
            SceneManager.LoadScene(SceneKey.Login);
        }
        else
        {
            NetworkPopUp.ShowPopUp("Delete Profile", response.message);
        }
    }

    private void OnClickNo()
    {
        PopUpController.ClosePopUp();
    }
}