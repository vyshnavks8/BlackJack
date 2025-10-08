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
    [SerializeField] private Image profileIcon;
    [SerializeField] private Button profileEditButton;
    [SerializeField] private Button profileButton;
    [SerializeField] private Image iconImage;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase profileCanvas;

    private string username;
    private string email;
    private string mobile;
    private bool uploadedImage;
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
    }

    private void SetData()
    {
        nameInput.text = username = AppData.username;
        emailInput.text = email = AppData.email;
        mobileInput.text = mobile = AppData.mobile;
        if (!string.IsNullOrEmpty(AppData.username))
        {
            profileImagText.text = AppData.username[0].ToString();
        }

        if (AppData.profileIcon != null)
        {
            profileIcon.sprite = AppData.profileIcon;
            profileIcon.gameObject.SetActive(true);
            profileImagText.gameObject.SetActive(false);
        }
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

    private void OnEditProfileClick()
    {
        StartCoroutine(BlackjackUtils.GetImageFromFile(OnReceiveImage));
    }

    private void OnReceiveImage(Texture2D texture, string path)
    {
        uploadedImage = true;
        profileImage = texture;
        iconImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        iconImage.gameObject.SetActive(true);
        profileImagText.gameObject.SetActive(false);
    }


    private void OnCancelClick()
    {
        OnSetCanvasActive(profileCanvas);
    }

    private void OnSubmitClick()
    {
        if (!CheckValidInputs()) return;
        if (ValidInputs()) return;
        UploadProfileData();
        UploadProfileImage();
    }

    private void UploadProfileData()
    {
        if (username == AppData.username && email == AppData.email && mobile == AppData.mobile) return;
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
        if (!uploadedImage)
        {
            LoadingController.HideLoading();
        }
        if (success)
        {
            if (response.success)
            {
                if (!uploadedImage)
                {
                    BlackJackApi.GetProfile(_ =>
                    {
                        LoadProfileCanvas();
                    });
                }
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

    private void UploadProfileImage()
    {
        if (!uploadedImage) return;
        LoadingController.ShowLoading();
        var data = BlackjackUtils.GetFormImage(profileImage, "image");
        APIHandler.PutForm<ProfileImageResponse>(ApiUrl.ProfileImage, data, OnProfileImageCallback);
    }

    private void OnProfileImageCallback(bool success, ProfileImageResponse response)
    {
        LoadingController.HideLoading();
        if (success)
        {
            if (response.success)
            {
                BlackJackApi.GetProfile(_ => { BlackJackApi.GetProfileIcon(AppData.iconUrl, LoadProfileCanvas); });
            }
            else
            {
                NetworkPopUp.ShowPopUp("Profile Image", response.message);
            }
        }
        else
        {
            NetworkPopUp.ShowPopUp("Profile Image", response.message);
        }
    }


    private void LoadProfileCanvas()
    {
        OnSetCanvasActive(profileCanvas);
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