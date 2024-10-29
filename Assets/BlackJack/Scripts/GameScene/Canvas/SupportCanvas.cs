using UnityEngine;
using UnityEngine.UI;

public class SupportCanvas : CanvasBase
{
    [SerializeField] private Button backButton;
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
        OnSetCanvasActive(transitionCanvas);
    }
}