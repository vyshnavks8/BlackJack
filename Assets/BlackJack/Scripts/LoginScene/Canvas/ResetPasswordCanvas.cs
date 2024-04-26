using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetPasswordCanvas : CanvasBase
{
    [SerializeField] private TMP_InputField newPasswordInput;
    [SerializeField] private TMP_InputField confirmPasswordInput;
    [SerializeField] private Button resetButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase loginCanvas;

    private string newPassword;
    private string confirmPassword;
    

    protected override void AddListener()
    {
        newPasswordInput.onValueChanged.AddListener(OnNewPasswordSet);
        confirmPasswordInput.onValueChanged.AddListener(OnConfirmPasswordSet);
        resetButton.onClick.AddListener(OnSubmitClick);
    }

    protected override void RemoveListener()
    {
        newPasswordInput.onValueChanged.RemoveListener(OnNewPasswordSet);
        confirmPasswordInput.onValueChanged.RemoveListener(OnConfirmPasswordSet);
        resetButton.onClick.RemoveListener(OnSubmitClick);
    }


    private void OnNewPasswordSet(string input)
    {
        newPassword = input;
    }

    private void OnConfirmPasswordSet(string input)
    {
        confirmPassword = input;
    }

    private void OnSubmitClick()
    {
        OnSetCanvasActive(loginCanvas);
    }
}