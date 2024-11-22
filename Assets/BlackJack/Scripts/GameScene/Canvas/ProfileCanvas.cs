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
        nameInput.text = AppData.username;
        emailInput.text = AppData.email;
        mobileInput.text = AppData.mobile;
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
        var popContent = new PopContent("", "Are you sure you want\nto <size=90><b>Delete</b></size> Account ?");
        var buttonContentA = new ButtonContent("No", OnClickNo);
        var buttonContentB = new ButtonContent("Yes", OnClickYes);
        PopUpController.ShowPopUp(popContent, buttonContentA, buttonContentB);
    }

    private void OnClickYes()
    {
        PopUpController.ClosePopUp();
        APIHandler.Delete<DeleteProfileResponse>(ApiUrl.DeleteProfile,null,OnDeleteAccountCallback);
       
    }

    private void OnDeleteAccountCallback(bool success, DeleteProfileResponse response)
    {
        if (response.success)
        {
            SceneManager.LoadScene(SceneKey.Login);
        }
        else
        {
            NetworkPopUp.ShowPopUp("Delete Profile",response.message);
        }
    }

    private void OnClickNo()
    {
        PopUpController.ClosePopUp();
    }
}