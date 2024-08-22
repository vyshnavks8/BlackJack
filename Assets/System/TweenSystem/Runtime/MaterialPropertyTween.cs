using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace RedDevil.Tween
{
    public class MaterialPropertyTween : MonoBehaviour
    {
        [SerializeField] private Renderer renderers;
        [SerializeField] private List<MaterialTweenData> tweenData = new();

        private void OnValidate()
        {
            if (renderers != null) return;
            var rendererGet = GetComponent<Renderer>();
            if (rendererGet == null)
            {
                return;
            }

            renderers = rendererGet;
        }


        private void OnEnable()
        {
            SetMaterialInstance();
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

        private void SetMaterialInstance()
        {
            foreach (var data in tweenData)
            {
                data.SetMaterialInstanceData(renderers);
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