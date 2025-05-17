using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayChoiceMenuUI : MonoBehaviour
{
    [FormerlySerializedAs("hitButton")]
    [Header("Buttons A")]
    [SerializeField] private Button buttonA;
    [SerializeField] private PlayerChoice playerChoiceA;
    [FormerlySerializedAs("standButton")]
    [Header("Buttons A")]
    [SerializeField] private Button buttonB;
    [SerializeField] private PlayerChoice playerChoiceB;
    [SerializeField] private RectTransform pivot;
    public event Action<PlayerChoice> OnPlayerChoice;
    

    private void OnEnable()
    {
        buttonA.onClick.AddListener(OnClickButtonA);
        buttonB.onClick.AddListener(OnClickButtonB);
    }

    private void OnDisable()
    {
        buttonA.onClick.RemoveListener(OnClickButtonA);
        buttonB.onClick.RemoveListener(OnClickButtonB);
    }

    public void ShowUI(bool show)
    {
        pivot.gameObject.SetActive(show);
    }
    
    private void OnClickButtonB()
    {
        OnPlayerChoice?.Invoke(playerChoiceB);
    }


    private void OnClickButtonA()
    {
        OnPlayerChoice?.Invoke(playerChoiceA);
    }
}