using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PopUpCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text headingText;
    [SerializeField] private TMP_Text dataText;
    [Header("Button A")] [SerializeField] private TMP_Text buttonTextA;
    [SerializeField] private Button buttonA;
    [Header("Button B")] [SerializeField] private TMP_Text buttonTextB;
    [SerializeField] private Button buttonB;
    [Header("Timer")] [SerializeField] private TMP_Text timerText;
    private IEnumerator enumerator;
    private Action buttonACallback, buttonBCallback,timerCallback;
    private void OnEnable()
    {
        buttonA.onClick.AddListener(OnClickPerformButton);
        buttonB.onClick.AddListener(OnClickCloseButton);
    }

    private void OnDisable()
    {
        buttonA.onClick.RemoveListener(OnClickPerformButton);
        buttonB.onClick.RemoveListener(OnClickCloseButton);
    }

    public void ShowPop(string heading, string data, string buttonNameA, Action callbackA,
        string buttonNameB, Action callbackB)
    {
        SetPopData(heading, data, buttonNameA, callbackA, buttonNameB, callbackB);
        gameObject.SetActive(true);
        SetTimerData();
    }
    public void ShowPop(string heading, string data, string buttonNameA, Action callbackA, string buttonNameB, Action callbackB,int timer , Action callbackC)
    {
        SetPopData(heading, data, buttonNameA, callbackA, buttonNameB, callbackB);
        gameObject.SetActive(true);
        SetTimerData(timer,callbackC);
    }

    public void ShowPop(string heading, string data,string buttonName,Action callback)
    {
        SetPopData(heading, data,buttonName,callback);
        gameObject.SetActive(true);
        SetTimerData();
    }
    public void ShowPop(string heading, string data)
    {
        SetPopData(heading, data);
        gameObject.SetActive(true);
        SetTimerData();
    }

    private void SetPopData(string heading, string data, string buttonNameA = null,
        Action callbackA = null, string buttonNameB = null, Action callbackB = null)
    {
        headingText.text = heading;
        dataText.text = data;
        buttonTextA.text = buttonNameA;
        buttonTextB.text = buttonNameB;
        buttonACallback = callbackA;
        buttonBCallback = callbackB;
        buttonA.gameObject.SetActive(buttonNameA != null);
        buttonB.gameObject.SetActive(buttonNameB != null);
    }

    private void SetTimerData(int value=0,Action callback=null)
    {
        if (value <= 0)
        {
            timerText.gameObject.SetActive(false);
            if (enumerator != null)
            {
                StopCoroutine(enumerator);
                enumerator = null;
            }
        }
        else
        {
            timerText.gameObject.SetActive(true);
            enumerator = StartTimer(value);
            StartCoroutine(enumerator);
        }
        timerCallback = callback;
    }
    private void OnClickPerformButton()
    {
        buttonACallback?.Invoke();
    }

    private void OnClickCloseButton()
    {
        buttonBCallback?.Invoke();
    }

    public void ClosePop()
    {
        SetPopData(string.Empty, string.Empty);
        SetTimerData();
        gameObject.SetActive(false);
    }
    private IEnumerator StartTimer(int time)
    {
        while (true)
        {
            if (time == 0)
            {
                timerCallback?.Invoke();
                yield break;
            }

            timerText.text = time+"s";
            time -= 1;
            yield return new WaitForSecondsRealtime(1);
        }
    }
}