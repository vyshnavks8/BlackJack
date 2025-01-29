using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditProfileCanvas : CanvasBase
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField mobileInput;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button backButton;

    [Header("Profile Image")] [SerializeField]
    private TMP_Text profileImagText;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase profileCanvas;

    private string username;
    private string email;
    private string mobile;

    protected override void OnEnable()
    {
        base.OnEnable();
        SetData();
    }

    private void SetData()
    {
        nameInput.text = username = AppData.username;
        emailInput.text = email = AppData.email;
        mobileInput.text = mobile = AppData.mobile;
        profileImagText.text = AppData.username[0].ToString();
    }


    protected override void AddListener()
    {
        nameInput.onValueChanged.AddListener(OnNameSet);
        emailInput.onValueChanged.AddListener(OnEmailSet);
        mobileInput.onValueChanged.AddListener(OnMobileSet);
        resetButton.onClick.AddListener(OnResetClick);
        cancelButton.onClick.AddListener(OnCancelClick);
        backButton.onClick.AddListener(OnCancelClick);
    }

    protected override void RemoveListener()
    {
        nameInput.onValueChanged.RemoveListener(OnNameSet);
        emailInput.onValueChanged.RemoveListener(OnEmailSet);
        mobileInput.onValueChanged.RemoveListener(OnMobileSet);
        resetButton.onClick.RemoveListener(OnResetClick);
        cancelButton.onClick.RemoveListener(OnCancelClick);
        backButton.onClick.RemoveListener(OnCancelClick);
    }


    private void OnNameSet(string input)
    {
        username = input;
    }

    private void OnEmailSet(string input)
    {
        email = input;
    }

    private void OnMobileSet(string input)
    {
        mobile = input;
    }


    private void OnCancelClick()
    {
        OnSetCanvasActive(profileCanvas);
    }

    private void OnResetClick()
    {
        if (!CheckValidInputs()) return;
        if (ValidInputs()) return;

        var data = new EditProfileData
        {
            name = username,
            email = email,
            mobileNo = mobile,
        };
        APIHandler.Put<EditProfileResponse>(ApiUrl.Profile, data, GetProfileCallback);
        LoadingController.ShowLoading();
    }

    private void GetProfileCallback(bool success, EditProfileResponse response)
    {
        LoadingController.HideLoading();
        if (success)
        {
            if (response.success)
            {
                OnSetCanvasActive(profileCanvas);
            }
            else
            {
                NetworkPopUp.ShowPopUp("Edit Profile", response.message);
            }
        }
        else
        {
            NetworkPopUp.ShowPopUp("Edit Profile", response.message);
        }
    }

    private bool ValidInputs()
    {
        if (!BlackjackUtils.IsValidEmail(email))
        {
            NetworkPopUp.ShowPopUp("Valid Email ID", "Please enter a valid email address");
            return true;
        }

        if (!BlackjackUtils.IsValidMobile(mobile))
        {
            NetworkPopUp.ShowPopUp("Valid Mobile", "Please enter a valid mobile number");
            return true;
        }


        return false;
    }


    private bool CheckValidInputs()
    {
        if (BlackjackUtils.IsInputEmpty(username, "Name")) return false;
        if (BlackjackUtils.IsInputEmpty(email, "Email ID")) return false;
        if (BlackjackUtils.IsInputEmpty(mobile, "Mobile Number")) return false;
        return true;
    }

    protected override void Close()
    {
        nameInput.text = null;
        emailInput.text = null;
        mobileInput.text = null;
    }
}