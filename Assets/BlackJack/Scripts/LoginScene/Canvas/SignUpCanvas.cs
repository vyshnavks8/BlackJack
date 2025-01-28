using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignUpCanvas : CanvasBase
{
    [SerializeField] private TMP_InputField fullNameInput;
    [SerializeField] private TMP_InputField loginInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_InputField confirmPasswordInput;
    [SerializeField] private Toggle rememberToggle;
    [SerializeField] private Button signUpButton;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button infoButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase loginCanvas;

    [SerializeField] private CanvasBase infoCanvas;
    private string fullName;
    private string emailID;
    private string mobileID;
    private string password;
    private string confirmPassword;
    private bool remember;


    protected override void AddListener()
    {
        fullNameInput.onValueChanged.AddListener(OnFullNameSet);
        loginInput.onValueChanged.AddListener(OnLoginSet);
        passwordInput.onValueChanged.AddListener(OnPasswordSet);
        confirmPasswordInput.onValueChanged.AddListener(OnConfirmPasswordSet);
        loginButton.onClick.AddListener(OnLoginClick);
        signUpButton.onClick.AddListener(OnSignUpClick);
        rememberToggle.onValueChanged.AddListener(OnRememberToggle);
        backButton.onClick.AddListener(OnBackClick);
        infoButton.onClick.AddListener(OnOpenInfo);
    }


    protected override void RemoveListener()
    {
        fullNameInput.onValueChanged.RemoveListener(OnFullNameSet);
        loginInput.onValueChanged.RemoveListener(OnLoginSet);
        passwordInput.onValueChanged.RemoveListener(OnPasswordSet);
        confirmPasswordInput.onValueChanged.RemoveListener(OnConfirmPasswordSet);
        loginButton.onClick.RemoveListener(OnLoginClick);
        signUpButton.onClick.RemoveListener(OnSignUpClick);
        rememberToggle.onValueChanged.RemoveListener(OnRememberToggle);
        backButton.onClick.RemoveListener(OnBackClick);
        infoButton.onClick.RemoveListener(OnOpenInfo);
    }


    private void OnOpenInfo()
    {
        InfoController.UpdateInfo("about", this);
        OnSetCanvasActive(infoCanvas);
    }
    private void OnFullNameSet(string input)
    {
        fullName = input;
    }
    private void OnLoginSet(string input)
    {
        
        if (BlackjackUtils.IsValidEmail(input))
        {
            emailID = input;
            mobileID = string.Empty;
        }
        else
        {
            emailID = string.Empty;
            mobileID = input;
        }
    }

    private void OnPasswordSet(string input)
    {
        password = input;
    }
    private void OnConfirmPasswordSet(string input)
    {
        confirmPassword = input;
    }

    private void OnRememberToggle(bool input)
    {
        remember = input;
    }
    

    private void OnSignUpClick()
    {
        if (skipApiCall)
        {
            OnSetCanvasActive(loginCanvas);
            return;
        }
        if (!CheckValidInputs()) return;
        if (password == confirmPassword)
        {
            var signUpData = new SignUpData
            {
                name = fullName,
                email = emailID,
                mobileNo = mobileID,
                password = password
            };
            APIHandler.Post<SignUpResponse>(ApiUrl.SignUp, signUpData, OnSignUpCallback);
        }
      
    }

    private bool CheckValidInputs()
    {
        if (BlackjackUtils.IsInputEmpty(fullName, "Display name"))     return false;
        if (string.IsNullOrEmpty(emailID) && string.IsNullOrEmpty(mobileID))
        {
            NetworkPopUp.ShowPopUp("Invalid Input","Email ID/ Mobile Number");
            return false;
        }
        if (BlackjackUtils.IsInputEmpty(password, "Password"))return false;
        if (BlackjackUtils.IsInputEmpty(confirmPassword, "Confirm Password"))return false;
        if (password != confirmPassword)
        {
            NetworkPopUp.ShowPopUp("Mismatch password","password doesnt match");
            return false;
        }
        return true;
    }

 
    private void OnSignUpCallback(bool success, SignUpResponse response)
    {
        if (success)
        {
            NetworkPopUp.ShowPopUp("Sign up", response.message);
            OnSetCanvasActive(loginCanvas);
        }
        else
        {
            NetworkPopUp.ShowPopUp("Sign up", response.message);
        }
    }

    private void OnLoginClick()
    {
        OnSetCanvasActive(loginCanvas);
    }

    private void OnBackClick()
    {
        OnSetCanvasActive(loginCanvas);
    }
    public override void Close()
    {
        fullNameInput.text = null;
        loginInput.text = null;
        passwordInput.text = null;
        confirmPasswordInput.text = null;
    }
}