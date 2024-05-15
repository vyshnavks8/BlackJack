using UnityEngine;
using UnityEngine.UI;

public class OtpCanvas : CanvasBase
{
    [SerializeField] private OtpFieldController otpFieldController;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase resetPasswordCanvas;[SerializeField]
    private CanvasBase forgotPasswordCanvas;

    private string otp;
    

    protected override void AddListener()
    {
        submitButton.onClick.AddListener(OnSubmitClick);
        backButton.onClick.AddListener(OnBackClick);
    }

    protected override void RemoveListener()
    {
        submitButton.onClick.RemoveListener(OnSubmitClick);
        backButton.onClick.RemoveListener(OnBackClick);
    }

    private void OnSubmitClick()
    {
        OnSetCanvasActive(resetPasswordCanvas);
    }
    private void OnBackClick()
    {
        OnSetCanvasActive(forgotPasswordCanvas);
    }
}