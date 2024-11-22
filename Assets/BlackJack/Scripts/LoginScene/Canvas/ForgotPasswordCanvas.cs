using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForgotPasswordCanvas : CanvasBase
{
    [SerializeField] private TMP_InputField loginInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button infoButton;
    private string loginID;

    [Header("Transition Canvas")] 
    [SerializeField]
    private CanvasBase loginCanvas;
    [SerializeField]
    private CanvasBase otpCanvas;
    [SerializeField]
    private CanvasBase infoCanvas;
    protected override void AddListener()
    {
        loginInput.onValueChanged.AddListener(OnLoginSet);
        submitButton.onClick.AddListener(OnSubmitClick);
        backButton.onClick.AddListener(OnBackClick); infoButton.onClick.AddListener(OnOpenInfo);
    }



    protected override void RemoveListener()
    {
        loginInput.onValueChanged.RemoveListener(OnLoginSet);
        submitButton.onClick.RemoveListener(OnSubmitClick);
        backButton.onClick.RemoveListener(OnBackClick);        infoButton.onClick.RemoveListener(OnOpenInfo);

    }
    private void OnOpenInfo()
    {
        InfoController.UpdateInfo("about",this);
        OnSetCanvasActive(infoCanvas);
    }
    private void OnLoginSet(string input)
    {
        loginID = input;
    }

    private void OnSubmitClick()
    {
        if (skipApiCall)
        {
            OnSetCanvasActive(otpCanvas);
            return;
        }
        if (!CheckValidInputs()) return;
        var forgotPasswordData = new ForgotPasswordData()
        {
            email = loginID,
        };
        APIHandler.Post<ForgotPasswordResponse>(ApiUrl.ForgotPassword, forgotPasswordData, OnForgotPasswordCallback);
       
    }

    private void OnForgotPasswordCallback(bool success, ForgotPasswordResponse response)
    {
        if (success)
        {
            ApiData.SetOtpToken(response.token);
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
        if (BlackjackUtils.IsInputEmpty(loginID, "Email ID/Mobile Number")) return false;
        return true;
    }
}