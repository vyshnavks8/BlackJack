using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetPasswordCanvas : CanvasBase
{
    [SerializeField] private TMP_InputField newPasswordInput;
    [SerializeField] private TMP_InputField confirmPasswordInput;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button infoButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase loginCanvas;

    [SerializeField] private CanvasBase infoCanvas;
    private string newPassword;
    private string confirmPassword;


    protected override void AddListener()
    {
        newPasswordInput.onValueChanged.AddListener(OnNewPasswordSet);
        confirmPasswordInput.onValueChanged.AddListener(OnConfirmPasswordSet);
        resetButton.onClick.AddListener(OnSubmitClick);
        infoButton.onClick.AddListener(OnOpenInfo);
    }

    protected override void RemoveListener()
    {
        newPasswordInput.onValueChanged.RemoveListener(OnNewPasswordSet);
        confirmPasswordInput.onValueChanged.RemoveListener(OnConfirmPasswordSet);
        resetButton.onClick.RemoveListener(OnSubmitClick);
        infoButton.onClick.RemoveListener(OnOpenInfo);
    }

    private void OnOpenInfo()
    {
        InfoController.UpdateInfo("about", this);
        OnSetCanvasActive(infoCanvas);
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
        if (skipApiCall)
        {
            OnSetCanvasActive(loginCanvas);
            return;
        }

        if (!CheckValidInputs()) return;
        var resetPasswordData = new ResetPasswordData
        {
            token = ApiData.ForgotPasswordToken,
            newPassword = newPassword,
        };
        APIHandler.Post<ResetPasswordResponse>(ApiUrl.ResetPassword, resetPasswordData, OnResetPasswordCallback);
        LoadingController.ShowLoading();
    }

    private void OnResetPasswordCallback(bool success, ResetPasswordResponse response)
    {
        LoadingController.HideLoading();
        if (success)
        {
            if (response.success)
            {
                OnSetCanvasActive(loginCanvas);
            }
            else
            {
                NetworkPopUp.ShowPopUp("Reset Password", response.message);
            }
        }
        else
        {
            NetworkPopUp.ShowPopUp("Reset Password", response.message);
        }
    }

    private bool CheckValidInputs()
    {
        if (BlackjackUtils.IsInputEmpty(newPassword, "Password")) return false;
        if (BlackjackUtils.IsInputEmpty(confirmPassword, "Confirm Password")) return false;
        if (newPassword != confirmPassword)
        {
            NetworkPopUp.ShowPopUp("Mismatch password", "password doesnt match");
            return false;
        }

        return true;
    }

    protected override void Close()
    {
        newPasswordInput.text = null;
        confirmPasswordInput.text = null;
        newPassword = null;
        confirmPassword = null;
    }
}