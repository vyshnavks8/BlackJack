using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoCanvas : CanvasBase
{
    [SerializeField] private TMP_Text headingText;
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private Button backButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase defaultTransitionCanvas;
    protected override void AddListener()
    {
        backButton.onClick.AddListener(OnBackClick);
    }

    protected override void RemoveListener()
    {
        backButton.onClick.RemoveListener(OnBackClick);
    }
    private void OnBackClick()
    {
        OnSetCanvasActive(defaultTransitionCanvas);
    }

    public void UpdateInfo(string heading, string info, CanvasBase canvasBase)
    {
        headingText.text = heading;
        infoText.text = info;
        defaultTransitionCanvas = canvasBase;
    }
}