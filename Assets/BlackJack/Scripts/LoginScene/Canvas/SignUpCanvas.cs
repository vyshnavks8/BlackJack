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

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase loginCanvas;

    private string loginID;
    private string password;
    private bool remember;


    protected override void AddListener()
    {
        loginInput.onValueChanged.AddListener(OnLoginSet);
        passwordInput.onValueChanged.AddListener(OnPasswordSet);
        loginButton.onClick.AddListener(OnLoginClick);
        signUpButton.onClick.AddListener(OnSignUpClick);
        rememberToggle.onValueChanged.AddListener(OnRememberToggle);
        backButton.onClick.AddListener(OnBackClick);
    }


    protected override void RemoveListener()
    {
        loginInput.onValueChanged.RemoveListener(OnLoginSet);
        passwordInput.onValueChanged.RemoveListener(OnPasswordSet);
        loginButton.onClick.RemoveListener(OnLoginClick);
        signUpButton.onClick.RemoveListener(OnSignUpClick);
        rememberToggle.onValueChanged.RemoveListener(OnRememberToggle);
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

    private void OnRememberToggle(bool input)
    {
        remember = input;
    }


    private void OnSignUpClick()
    {
        SceneManager.LoadScene(SceneKey.Game);
    }

    private void OnLoginClick()
    {
        OnSetCanvasActive(loginCanvas);
    }
    private void OnBackClick()
    {
        OnSetCanvasActive(loginCanvas);
    }
}