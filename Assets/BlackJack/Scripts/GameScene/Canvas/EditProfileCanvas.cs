using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EditProfileCanvas : CanvasBase
{
    [SerializeField] private AppDataSO appDataSo;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField mobileInput;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button backButton;

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
        nameInput.text = appDataSo.username;
        emailInput.text = appDataSo.email;
        mobileInput.text = appDataSo.mobile;
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
        appDataSo.SetUserData(username,email,mobile);
        OnSetCanvasActive(profileCanvas);
    }
}