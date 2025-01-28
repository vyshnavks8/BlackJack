using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginCanvas : CanvasBase
{
    [SerializeField] private TMP_InputField loginInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button forgotPasswordButton;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signUpButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button infoButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase forgotPasswordCanvas;

    [SerializeField] private CanvasBase signUpCanvas;
    [SerializeField] private CanvasBase welcomeCanvas;
    [SerializeField] private CanvasBase infoCanvas;

    private string emailID;
    private string mobileID;
    private string password;
    private bool email;

    protected override void AddListener()
    {
        loginInput.onValueChanged.AddListener(OnLoginSet);
        passwordInput.onValueChanged.AddListener(OnPasswordSet);
        loginButton.onClick.AddListener(OnLoginClick);
        forgotPasswordButton.onClick.AddListener(OnForgotPasswordClick);
        signUpButton.onClick.AddListener(OnSignUpClick);
        backButton.onClick.AddListener(OnBackClick);
        infoButton.onClick.AddListener(OnOpenInfo);
    }

    protected override void RemoveListener()
    {
        loginInput.onValueChanged.RemoveListener(OnLoginSet);
        passwordInput.onValueChanged.RemoveListener(OnPasswordSet);
        loginButton.onClick.RemoveListener(OnLoginClick);
        forgotPasswordButton.onClick.RemoveListener(OnForgotPasswordClick);
        signUpButton.onClick.RemoveListener(OnSignUpClick);
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

    private void OnPasswordSet(string input)
    {
        password = input;
    }

    private void OnForgotPasswordClick()
    {
        OnSetCanvasActive(forgotPasswordCanvas);
    }

    private void OnSignUpClick()
    {
        OnSetCanvasActive(signUpCanvas);
    }

    private void OnLoginClick()
    {
        if (skipApiCall)
        {
            SceneManager.LoadScene(SceneKey.Game);
            return;
        }

        if (!CheckValidInputs()) return;
        if (email)
        {
            var loginData = new LoginDataEmail
            {
                email = emailID,
                password = password
            };
            APIHandler.Post<LoginResponse>(ApiUrl.Login, loginData, OnLoginCallback, true);
        }
        else
        {
            var loginData = new LoginDataMobile
            {
                mobileNo = mobileID,
                password = password
            };
            APIHandler.Post<LoginResponse>(ApiUrl.Login, loginData, OnLoginCallback, true);
        }
    }

    private void OnLoginCallback(bool success, LoginResponse response)
    {
        if (success)
        {
            BlackJackSave.SetLoginToken(response.token);
            BlackJackApi.GetProfile(OnRecievedProfile);
           
        }
        else
        {
            NetworkPopUp.ShowPopUp("Login", response.message);
        }
    }

    private void OnRecievedProfile(bool success)
    {
        if (success)
        {
            SceneManager.LoadScene(SceneKey.Game);
        }
    }


    private void OnBackClick()
    {
        OnSetCanvasActive(welcomeCanvas);
    }

    private bool CheckValidInputs()
    {
        if (string.IsNullOrEmpty(emailID) && string.IsNullOrEmpty(mobileID))
        {
            NetworkPopUp.ShowPopUp("Invalid Input","Email ID/ Mobile Number");
            return false;
        }
        if (BlackjackUtils.IsInputEmpty(password, "Password")) return false;
        return true;
    }

    protected override void Close()
    {
        loginInput.text = null;
        passwordInput.text = null;
    }
}