using UnityEngine;
using UnityEngine.UI;

public class OtpCanvas : CanvasBase
{
    [SerializeField] private OtpFieldController otpFieldController;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button infoButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase resetPasswordCanvas;

    [SerializeField] private CanvasBase forgotPasswordCanvas;
    [SerializeField] private CanvasBase infoCanvas;

    protected override void AddListener()
    {
        submitButton.onClick.AddListener(OnSubmitClick);
        backButton.onClick.AddListener(OnBackClick);
        infoButton.onClick.AddListener(OnOpenInfo);
    }

    protected override void RemoveListener()
    {
        submitButton.onClick.RemoveListener(OnSubmitClick);
        backButton.onClick.RemoveListener(OnBackClick);
        infoButton.onClick.RemoveListener(OnOpenInfo);
    }

    private void OnOpenInfo()
    {
        InfoController.UpdateInfo("about", this);
        OnSetCanvasActive(infoCanvas);
    }

    private void OnSubmitClick()
    {
        if (skipApiCall)
        {
            OnSetCanvasActive(resetPasswordCanvas);
            return;
        }
        if (!CheckValidInputs()) return;
        var otpData = new OtpData
        {
            token = ApiData.ForgotPasswordToken,
            otp = otpFieldController.Otp,
        };
        APIHandler.Post<OtpResponse>(ApiUrl.Otp, otpData, OnOtpResponseCallback);

    }

    private void OnOtpResponseCallback(bool success, OtpResponse response)
    {
        if (success)
        {
            
            OnSetCanvasActive(resetPasswordCanvas);
        }
        else
        {
            NetworkPopUp.ShowPopUp("OTP", response.message);
        }
    }

    private void OnBackClick()
    {
        OnSetCanvasActive(forgotPasswordCanvas);
    }
    private bool CheckValidInputs()
    {
        if (BlackjackUtils.IsInputEmpty(otpFieldController.Otp, "OTP")) return false;
        return true;
    }
    public override void Close()
    {
        otpFieldController.ClearOtpBox();
    }
}