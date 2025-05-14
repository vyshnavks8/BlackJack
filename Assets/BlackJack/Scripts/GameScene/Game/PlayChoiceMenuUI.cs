using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayChoiceMenuUI : MonoBehaviour
{
    [SerializeField] private Button hitButton;
    [SerializeField] private Button standButton;
    [SerializeField] private RectTransform pivot;
    public event Action<PlayerChoice> OnPlayerChoice;
    

    private void OnEnable()
    {
        hitButton.onClick.AddListener(OnClickHitButton);
        standButton.onClick.AddListener(OnClickStandButton);
    }

    private void OnDisable()
    {
        hitButton.onClick.RemoveListener(OnClickHitButton);
        standButton.onClick.RemoveListener(OnClickStandButton);
    }

    public void ShowUI(bool show)
    {
        pivot.gameObject.SetActive(show);
    }
    
    private void OnClickStandButton()
    {
        OnPlayerChoice?.Invoke(PlayerChoice.Stand);
    }


    private void OnClickHitButton()
    {
        OnPlayerChoice?.Invoke(PlayerChoice.Hit);
    }
}