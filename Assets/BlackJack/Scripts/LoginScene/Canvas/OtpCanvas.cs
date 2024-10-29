using UnityEngine;
using UnityEngine.UI;

public class OtpCanvas : CanvasBase
{
    [SerializeField] private OtpFieldController otpFieldController;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button infoButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase resetPasswordCanvas;

    [SerializeField] private CanvasBase forgotPasswordCanvas;
    [SerializeField] private CanvasBase infoCanvas;
    private string otp;


    protected override void AddListener()
    {
        submitButton.onClick.AddListener(OnSubmitClick);
        backButton.onClick.AddListener(OnBackClick);
        infoButton.onClick.AddListener(OnOpenInfo);
    }

    protected override void RemoveListener()
    {
        submitButton.onClick.RemoveListener(OnSubmitClick);
        backButton.onClick.RemoveListener(OnBackClick);
        infoButton.onClick.RemoveListener(OnOpenInfo);
    }

    private void OnOpenInfo()
    {
        InfoController.UpdateInfo("about", this);
        OnSetCanvasActive(infoCanvas);
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