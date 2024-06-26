using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginCanvas : CanvasBase
{
    [SerializeField] private TMP_InputField loginInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Toggle rememberToggle;
    [SerializeField] private Button forgotPasswordButton;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signUpButton;
    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase forgotPasswordCanvas;[SerializeField]
    private CanvasBase signUpCanvas;

    private string loginID;
    private string password;
    private bool remember;

    protected override void AddListener()
    {
        loginInput.onValueChanged.AddListener(OnLoginSet);
        passwordInput.onValueChanged.AddListener(OnPasswordSet);
        loginButton.onClick.AddListener(OnLoginClick);
        forgotPasswordButton.onClick.AddListener(OnForgotPasswordClick);
        rememberToggle.onValueChanged.AddListener(OnRememberToggle);
        signUpButton.onClick.AddListener(OnSignUpClick);
    }

    protected override void RemoveListener()
    {
        loginInput.onValueChanged.RemoveListener(OnLoginSet);
        passwordInput.onValueChanged.RemoveListener(OnPasswordSet);
        loginButton.onClick.RemoveListener(OnLoginClick);
        forgotPasswordButton.onClick.RemoveListener(OnForgotPasswordClick);
        rememberToggle.onValueChanged.RemoveListener(OnRememberToggle);
        signUpButton.onClick.RemoveListener(OnSignUpClick);
    }

    private void OnLoginSet(string input)
    {
        loginID = input;
    }

    private void OnPasswordSet(string input)
    {
        password = input;
    }
    private void OnRememberToggle(bool input)
    {
        remember = input;
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
        SceneManager.LoadScene(SceneKey.Game);
    }
}