using UnityEngine;
using UnityEngine.UI;

public class OtpCanvas : CanvasBase
{
    [SerializeField] private OtpFieldController otpFieldController;
    [SerializeField] private Button submitButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase resetPasswordCanvas;

    private string otp;
    

    protected override void AddListener()
    {
        submitButton.onClick.AddListener(OnSubmitClick);
    }

    protected override void RemoveListener()
    {
        submitButton.onClick.RemoveListener(OnSubmitClick);
    }

    private void OnSubmitClick()
    {
        OnSetCanvasActive(resetPasswordCanvas);
    }
}