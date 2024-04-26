using UnityEngine;
using UnityEngine.UI;

public class WelcomeCanvas : CanvasBase
{
    [SerializeField] private Button loginButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase loginCanvas;
    protected override void AddListener()
    {
        loginButton.onClick.AddListener(OnLoginClick);
    }

    protected override void RemoveListener()
    {
        loginButton.onClick.RemoveListener(OnLoginClick);
    }


    private void OnLoginClick()
    {
        OnSetCanvasActive(loginCanvas);
    }
}