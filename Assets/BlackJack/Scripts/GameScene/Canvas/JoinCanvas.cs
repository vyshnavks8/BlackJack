using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinCanvas : CanvasBase
{
    [SerializeField] private TMP_InputField roomInput;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button backButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase privateCanvas;

    [SerializeField] private CanvasBase gameCanvas;

    protected override void AddListener()
    {
        roomInput.onValueChanged.AddListener(OnRoomInput);
        joinButton.onClick.AddListener(OnJoinClick);
        backButton.onClick.AddListener(OnBackClick);
    }

    protected override void RemoveListener()
    {
        roomInput.onValueChanged.RemoveListener(OnRoomInput);
        joinButton.onClick.RemoveListener(OnJoinClick);
        backButton.onClick.RemoveListener(OnBackClick);
    }

    private void OnRoomInput(string value)
    {
        
    }

    private void OnBackClick()
    {
        OnSetCanvasActive(privateCanvas);
    }

    private void OnJoinClick()
    {
        OnSetCanvasActive(gameCanvas);
    }
    
}