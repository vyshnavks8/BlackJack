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
        nameInput.text = AppData.username;
        emailInput.text = AppData.email;
        mobileInput.text = AppData.mobile;
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
        var data = new EditProfileData
        {
            name = username,
            email = email,
            mobileNo = mobile,
        };
        APIHandler.Put<EditProfileResponse>(ApiUrl.Profile, data, GetProfileCallback);
    }

    private void GetProfileCallback(bool success, EditProfileResponse data)
    {
        if (success)
        {
            NetworkPopUp.ShowPopUp("Edit Profile", data.message);
            OnSetCanvasActive(profileCanvas);
        }
        else
        {
            NetworkPopUp.ShowPopUp("Edit Profile", data.message);
        }
    }
}