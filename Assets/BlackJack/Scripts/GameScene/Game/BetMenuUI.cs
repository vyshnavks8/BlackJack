using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetMenuUI : MonoBehaviour
{
    [SerializeField] private ChipDataSO chipDataSO;
    [SerializeField] private ChipUI chipUI;
    [SerializeField] private Button betButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private RectTransform pivot;
    public event Action<int> OnBet;
    private int betAmount;
    public int selectedID;
    private bool ReachedMax => selectedID == chipDataSO.Count - 1;
    private bool ReachedMin => selectedID == 0;
    

    private void OnEnable()
    {
        InitBetData();
        betButton.onClick.AddListener(OnClickBetButton);
        leftButton.onClick.AddListener(OnClickLeftButton);
        rightButton.onClick.AddListener(OnClickRightButton);
    }


    private void OnDisable()
    {
        InitBetData();
        betButton.onClick.RemoveListener(OnClickBetButton);
        leftButton.onClick.RemoveListener(OnClickLeftButton);
        rightButton.onClick.RemoveListener(OnClickRightButton);
    }

    private void OnClickLeftButton()
    {
        if (ReachedMin) return;
        selectedID -= 1;
        SetBetData();
    }

    private void OnClickRightButton()
    {
        if (ReachedMax) return;
        selectedID += 1;
        SetBetData();
    }

    private void OnClickBetButton()
    {
        OnBet?.Invoke(betAmount);
    }


    private void InitBetData()
    {
        selectedID = 0;
        SetBetData();
    }

    private void SetBetData()
    {
        SetButtonInteractable();
        betAmount = chipDataSO.GetChipAmount(selectedID);
        chipUI.SetChipValue(betAmount);
    }
    

    private void SetButtonInteractable()
    {
        if (ReachedMax)
        {
            leftButton.interactable = true;
            rightButton.interactable = false;
        }
        else if (ReachedMin)
        {
            leftButton.interactable = false;
            rightButton.interactable = true;
        }
        else
        {
            leftButton.interactable = true;
            rightButton.interactable = true;
        }
    }

    public void ShowUI(bool show)
    {
        pivot.gameObject.SetActive(show);
    }
}