using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ScoreboardCanvas : CanvasBase
{
    [SerializeField] private ScoreHolder scoreHolderPrefab;
    [SerializeField] private RectTransform scrollContentTransform;
    [SerializeField] private Button backButton;

    [Header("Transition Canvas")] [SerializeField]
    private CanvasBase gameCanvas;

    private void Awake()
    {
        for (int i = 1; i <= 20; i++)
        {
            var chatInstance = Instantiate(scoreHolderPrefab, scrollContentTransform);
            chatInstance.SetData(i.ToString(), "name " + i, Random.Range(10, 8000).ToString());
        }
    }

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
        OnSetCanvasActive(gameCanvas);
    }
}