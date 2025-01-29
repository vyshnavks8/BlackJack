using RestAPI;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        var token = BlackJackSave.GetLogin();
        if (!string.IsNullOrEmpty(token))
        {
            LoadingController.ShowLoading();
            ApiBase.SetAuthToken(token);
            BlackJackApi.GetProfile(OnGetProfile);
        }
        else
        {
            OnSetCanvasActive(loginCanvas);
        }
    }

    private void OnGetProfile(bool valid)
    {
        LoadingController.HideLoading();
        if (valid)
        {
            SceneManager.LoadScene(SceneKey.Game);
        }
        else
        {
            OnSetCanvasActive(loginCanvas);
        }
    }
}