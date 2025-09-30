using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditProfileCanvas : CanvasBase
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField mobileInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button backButton;

    [Header("Profile")] [SerializeField] private TMP_Text profileImagText;
    [SerializeField] private Button profileEditButton;
    [SerializeField] private Button profileButton;
    [SerializeField] private Image iconImage;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase profileCanvas;

    private string username;
    private string email;
    private string mobile;
    private bool uploadedImage;
    private string profileImagePath;
    private Texture2D profileImage;

    protected override void OnEnable()
    {
        base.OnEnable();
        SetData();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        uploadedImage = false;
        profileImagePath = null;
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
        submitButton.onClick.AddListener(OnSubmitClick);
        cancelButton.onClick.AddListener(OnCancelClick);
        backButton.onClick.AddListener(OnCancelClick);
        profileEditButton.onClick.AddListener(OnEditProfileClick);
        profileButton.onClick.AddListener(OnEditProfileClick);
    }


    protected override void RemoveListener()
    {
        nameInput.onValueChanged.RemoveListener(OnNameSet);
        emailInput.onValueChanged.RemoveListener(OnEmailSet);
        mobileInput.onValueChanged.RemoveListener(OnMobileSet);
        submitButton.onClick.RemoveListener(OnSubmitClick);
        cancelButton.onClick.RemoveListener(OnCancelClick);
        backButton.onClick.RemoveListener(OnCancelClick);
        profileEditButton.onClick.RemoveListener(OnEditProfileClick);
        profileButton.onClick.RemoveListener(OnEditProfileClick);
    }

    private void OnEditProfileClick()
    {
        StartCoroutine(BlackjackUtils.GetImageFromFile(OnReceiveImage));
    }

    private void OnReceiveImage(Texture2D texture,string path)
    {
        uploadedImage = true;
        profileImage = texture;
        profileImagePath = path;
        iconImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        iconImage.gameObject.SetActive(true);
        profileImagText.gameObject.SetActive(false);
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

    private void OnSubmitClick()
    {
        if (!CheckValidInputs()) return;
        if (ValidInputs()) return;
        UploadProfileImage();
        var data = new EditProfileData
        {
            name = username,
            email = email,
            mobileNo = mobile,
        };
        APIHandler.Put<EditProfileResponse>(ApiUrl.Profile, data, GetProfileCallback);

        LoadingController.ShowLoading();
    }

    private void UploadProfileImage()
    {
        if (!uploadedImage) return;
        var data = BlackjackUtils.GetFormImage(profileImagePath, "image");
        APIHandler.Put<ProfileImageResponse>(ApiUrl.ProfileImagePut, data, OnProfileImageCallback);
    }

    private void OnProfileImageCallback(bool success, ProfileImageResponse response)
    {
        if (success)
        {
            if (response.success)
            {
                Debug.Log("Profile image updated" + response.data.profileImage);
                uploadedImage = false;
                profileImagePath = null;
            }
            else
            {
                Debug.Log("Profile image updated" + response.message);
            }
        }
        else
        {
            Debug.Log("Profile image updated" + response.message);
        }
    }

    private void GetProfileCallback(bool success, EditProfileResponse response)
    {
        LoadingController.HideLoading();
        if (success)
        {
            if (response.success)
            {
                Debug.Log("Profile" + response.message);
                //OnSetCanvasActive(profileCanvas);
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
        if (!string.IsNullOrEmpty(email) && !BlackjackUtils.IsValidEmail(email))
        {
            NetworkPopUp.ShowPopUp("Valid Email ID", "Please enter a valid email address");
            return true;
        }


        if (!string.IsNullOrEmpty(mobile) && !BlackjackUtils.IsValidMobile(mobile))
        {
            NetworkPopUp.ShowPopUp("Valid Mobile", "Please enter a valid mobile number");
            return true;
        }


        return false;
    }


    private bool CheckValidInputs()
    {
        if (BlackjackUtils.IsInputEmpty(username, "Name")) return false;
        if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(mobile))
        {
            BlackjackUtils.ShowEmpty("Email ID or Cell");
            return false;
        }
        return true;
    }

    protected override void Close()
    {
        nameInput.text = null;
        emailInput.text = null;
        mobileInput.text = null;
    }
}