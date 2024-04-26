using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SplashCanvas : MonoBehaviour
{
    [SerializeField] private string loadSceneName;
    [SerializeField] private bool loadOnStart;
    [Header("Fake Load")] [SerializeField] private bool fakeLoading;
    [SerializeField] private float minLoadFrequency = 0.4f;
    [SerializeField] private float maxLoadFrequency = 0.6f;
    [SerializeField] private float loadTime = 10f;
    private IEnumerator loadSceneEnumerator;

    private delegate void LoadingFinished();

    public UnityEvent<float> OnLoading;

    private void Start()
    {
        if (loadOnStart)
        {
            StartLoading();
        }
    }

    public void StartLoading()
    {
        loadSceneEnumerator = fakeLoading ? FakeLoading(0.0f, LoadLevel) : AsyncLoading();
        StartCoroutine(loadSceneEnumerator);
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(loadSceneName);
    }

    private IEnumerator FakeLoading(float value, LoadingFinished loadingFinished)
    {
        while (true)
        {
            var random = Random.Range(minLoadFrequency, maxLoadFrequency);
            value += random;
            if (value > 1.0f)
            {
                value = 1.0f;
                OnLoading?.Invoke(value);
                loadingFinished?.Invoke();
                yield break;
            }

            OnLoading?.Invoke(value);
            yield return new WaitForSecondsRealtime(loadTime);
        }
    }

    private IEnumerator AsyncLoading()
    {
        var asyncLoad = SceneManager.LoadSceneAsync(loadSceneName);
        while (!asyncLoad.isDone)
        {
            var value = Mathf.Clamp01(asyncLoad.progress / .9f);
            OnLoading?.Invoke(value);
            yield return null;
        }
    }
}