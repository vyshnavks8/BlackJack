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
    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase forgotPasswordCanvas;[SerializeField]
    private CanvasBase signUpCanvas;
    [SerializeField]
    private CanvasBase welcomeCanvas;

    private string loginID;
    private string password;

    protected override void AddListener()
    {
        loginInput.onValueChanged.AddListener(OnLoginSet);
        passwordInput.onValueChanged.AddListener(OnPasswordSet);
        loginButton.onClick.AddListener(OnLoginClick);
        forgotPasswordButton.onClick.AddListener(OnForgotPasswordClick);
        signUpButton.onClick.AddListener(OnSignUpClick);
        backButton.onClick.AddListener(OnBackClick);
    }

    protected override void RemoveListener()
    {
        loginInput.onValueChanged.RemoveListener(OnLoginSet);
        passwordInput.onValueChanged.RemoveListener(OnPasswordSet);
        loginButton.onClick.RemoveListener(OnLoginClick);
        forgotPasswordButton.onClick.RemoveListener(OnForgotPasswordClick);
        signUpButton.onClick.RemoveListener(OnSignUpClick);
        backButton.onClick.RemoveListener(OnBackClick);
    }

    private void OnLoginSet(string input)
    {
        loginID = input;
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
        SceneManager.LoadScene(SceneKey.Game);
    }
    private void OnBackClick()
    {
        OnSetCanvasActive(welcomeCanvas);
    }
}