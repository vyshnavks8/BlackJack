using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RedDevil.Tween
{
    public class UIMaterialTween : MonoBehaviour
    {
        [SerializeField] private List<UIMaterialTweenData> tweenData = new();
        private void OnEnable()
        {
            foreach (var data in tweenData)
            {
                data.InitPropertyValue();
                data.SetEnableValue();
            }
        }

        private void OnDisable()
        {
            foreach (var data in tweenData)
            {
                data.InitPropertyValue();
                data.SetDisableValue();
            }
        }

        public void StartTween(int id)
        {
            foreach (var data in tweenData.Where(data => data.tweenID == id))
            {
                data.StartTween();
                if (data.autoReset)
                {
                    StartTimerCallback(data.resetTimer, () => data.ResetTween());
                }
            }
        }

        public void ResetTween(int id)
        {
            foreach (var data in tweenData.Where(data => data.tweenID == id))
            {
                data.ResetTween();
            }
        }
        private void StartTimerCallback(float time, Action action)
        {
            var enumerator = StartTimer(time, action);
            StartCoroutine(enumerator);
        }
        private IEnumerator StartTimer(float f, Action action)
        {
            yield return new WaitForSeconds(f);
            action?.Invoke();
        }
    }
}