using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CoolDownTimerUI : MonoBehaviour
{
    [SerializeField] private Image coolDownImage;
    private float currentCooldownTime = -1f;
    private bool countDownStartedOnce;
    private bool PPauseActionTimer { get; set; }
    private bool TimerResetsOnEveryStart { get; set; }
    private IEnumerator coolDownTimer;

    private void Awake()
    {
        currentCooldownTime = -1f;
        TimerResetsOnEveryStart = true;
        coolDownImage.fillAmount = 0;
    }

    private void OnValidate()
    {
        if (coolDownImage == null)
        {
            coolDownImage = GetComponent<Image>();
        }
        coolDownImage.fillAmount = 0;
    }
    

    public void StartCooldownTimer(float totalDuration, Action response = null)
    {
        StopCooldownTimer();
        coolDownTimer = StartCooldownTimerInternal(response, totalDuration);
        StartCoroutine(coolDownTimer);
    }

    public void StopCooldownTimer()
    {
        if (TimerResetsOnEveryStart)
        {
            currentCooldownTime = -1f;
        }

        coolDownImage.fillAmount = 0f;
        if (coolDownTimer == null) return;
        StopCoroutine(coolDownTimer);
        coolDownTimer = null;
    }
    
    private IEnumerator StartCooldownTimerInternal(Action response, float totalDuration, float startDuration = 0f)
    {
        if (TimerResetsOnEveryStart)
        {
            currentCooldownTime = startDuration == 0f ? totalDuration : startDuration;
        }
        else
        {
            if (!countDownStartedOnce)
            {
                countDownStartedOnce = true;
                currentCooldownTime = startDuration == 0f ? totalDuration : startDuration;
            }
        }

        coolDownImage.fillAmount = currentCooldownTime / totalDuration;
        while (currentCooldownTime > 0.0f)
        {
            if (!PPauseActionTimer)
            {
                currentCooldownTime -= Time.deltaTime;
                coolDownImage.fillAmount = currentCooldownTime / totalDuration;
            }

            yield return new WaitForEndOfFrame();
        }
        
        currentCooldownTime = -1f;
        response?.Invoke();
    }
}