using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePasswordCanvas : CanvasBase
{
    [SerializeField] private TMP_InputField newPasswordInput;
    [SerializeField] private TMP_InputField confirmPasswordInput;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button backButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase profileCanvas;

    private string newPassword;
    private string confirmPassword;

    protected override void AddListener()
    {
        newPasswordInput.onValueChanged.AddListener(OnNewPasswordSet);
        confirmPasswordInput.onValueChanged.AddListener(OnConfirmPasswordSet);
        resetButton.onClick.AddListener(OnResetClick);
        cancelButton.onClick.AddListener(OnCancelClick);
        backButton.onClick.AddListener(OnCancelClick);
    }

    protected override void RemoveListener()
    {
        newPasswordInput.onValueChanged.RemoveListener(OnNewPasswordSet);
        confirmPasswordInput.onValueChanged.RemoveListener(OnConfirmPasswordSet);
        resetButton.onClick.RemoveListener(OnResetClick);
        cancelButton.onClick.RemoveListener(OnCancelClick);
        backButton.onClick.RemoveListener(OnCancelClick);
    }


    private void OnNewPasswordSet(string input)
    {
        newPassword = input;
    }

    private void OnConfirmPasswordSet(string input)
    {
        confirmPassword = input;
    }

    private void OnCancelClick()
    {
        OnSetCanvasActive(profileCanvas);
    }

    private void OnResetClick()
    {
        OnSetCanvasActive(profileCanvas);
    }
}