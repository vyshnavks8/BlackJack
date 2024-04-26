using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForgotPasswordCanvas : CanvasBase
{
    [SerializeField] private TMP_InputField loginInput;
    [SerializeField] private Button submitButton;
    private string loginID;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase otpCanvas;
    
    protected override void AddListener()
    {
        loginInput.onValueChanged.AddListener(OnLoginSet);
        submitButton.onClick.AddListener(OnSubmitClick);
    }

    protected override void RemoveListener()
    {
        loginInput.onValueChanged.RemoveListener(OnLoginSet);
        submitButton.onClick.RemoveListener(OnSubmitClick);
    }

    private void OnLoginSet(string input)
    {
        loginID = input;
    }

    private void OnSubmitClick()
    {
        OnSetCanvasActive(otpCanvas);
    }
}