using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForgotPasswordCanvas : CanvasBase
{
    [SerializeField] private TMP_InputField loginInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;
    private string loginID;

    [Header("Transition Canvas")] 
    [SerializeField]
    private CanvasBase loginCanvas;
    [SerializeField]
    private CanvasBase otpCanvas;
    
    protected override void AddListener()
    {
        loginInput.onValueChanged.AddListener(OnLoginSet);
        submitButton.onClick.AddListener(OnSubmitClick);
        backButton.onClick.AddListener(OnBackClick);
    }



    protected override void RemoveListener()
    {
        loginInput.onValueChanged.RemoveListener(OnLoginSet);
        submitButton.onClick.RemoveListener(OnSubmitClick);
        backButton.onClick.RemoveListener(OnBackClick);
    }

    private void OnLoginSet(string input)
    {
        loginID = input;
    }

    private void OnSubmitClick()
    {
        OnSetCanvasActive(otpCanvas);
    }
    private void OnBackClick()
    {
        OnSetCanvasActive(loginCanvas);
    }
}