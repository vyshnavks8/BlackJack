using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForgotPasswordCanvas : CanvasBase
{
    [SerializeField] private TMP_InputField loginInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button infoButton;


    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase loginCanvas;

    [SerializeField] private CanvasBase otpCanvas;
    [SerializeField] private CanvasBase infoCanvas;

    private string emailID;
    private string mobileID;
    private bool email;


    protected override void AddListener()
    {
        loginInput.onValueChanged.AddListener(OnLoginSet);
        submitButton.onClick.AddListener(OnSubmitClick);
        backButton.onClick.AddListener(OnBackClick);
        infoButton.onClick.AddListener(OnOpenInfo);
    }


    protected override void RemoveListener()
    {
        loginInput.onValueChanged.RemoveListener(OnLoginSet);
        submitButton.onClick.RemoveListener(OnSubmitClick);
        backButton.onClick.RemoveListener(OnBackClick);
        infoButton.onClick.RemoveListener(OnOpenInfo);
    }

    private void OnOpenInfo()
    {
        InfoController.UpdateInfo("about", this);
        OnSetCanvasActive(infoCanvas);
    }

    private void OnLoginSet(string input)
    {
        if (BlackjackUtils.IsValidEmail(input))
        {
            emailID = input;
            mobileID = string.Empty;
            email = true;
        }
        else
        {
            emailID = string.Empty;
            mobileID = input;
            email = false;
        }
    }

    private void OnSubmitClick()
    {
        if (skipApiCall)
        {
            OnSetCanvasActive(otpCanvas);
            return;
        }

        if (!CheckValidInputs()) return;
        if (email)
        {
            var forgotPasswordData = new ForgotPasswordDataEmail()
            {
                email = emailID,
            };
            APIHandler.Post<ForgotPasswordResponse>(ApiUrl.ForgotPassword, forgotPasswordData, OnForgotPasswordCallback,
                true);
        }
        else
        {
            var forgotPasswordData = new ForgotPasswordDataMobile()
            {
                mobileNo = mobileID,
            };
            APIHandler.Post<ForgotPasswordResponse>(ApiUrl.ForgotPassword, forgotPasswordData, OnForgotPasswordCallback,
                true);
        }
    }

    private void OnForgotPasswordCallback(bool success, ForgotPasswordResponse response)
    {
        if (success)
        {
            ApiData.SetForgotPasswordToken(response.token);
            Debug.Log("OTP " + response.otp);
            NetworkPopUp.ShowPopUp("Forgot Password", response.message);
            OnSetCanvasActive(otpCanvas);
        }
        else
        {
            NetworkPopUp.ShowPopUp("Forgot Password", response.message);
        }
    }

    private void OnBackClick()
    {
        OnSetCanvasActive(loginCanvas);
    }

    private bool CheckValidInputs()
    {
        if (string.IsNullOrEmpty(emailID) && string.IsNullOrEmpty(mobileID))
        {
            NetworkPopUp.ShowPopUp("Invalid Input", "Email ID/ Mobile Number");
            return false;
        }

        return true;
    }

    public override void Close()
    {
        loginInput.text = null;
    }
}