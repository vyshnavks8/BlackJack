using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private LoadingCanvas loadingPanel;
    private static LoadingManager loadingManager;
    private void Awake()
    {
        if (loadingManager == null)
        {
            loadingManager = this;
            DontDestroyOnLoad(this);
        }
        else if (loadingManager != null)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        LoadingController.OnLoading += OnLoading;
    }


    private void OnDisable()
    {
        LoadingController.OnLoading -= OnLoading;
    }
    private void OnLoading(bool load)
    {
        loadingPanel.ShowLoading(load);
    }

}