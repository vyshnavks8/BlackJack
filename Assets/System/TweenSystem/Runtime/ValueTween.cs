using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace RedDevil.Tween
{
    public class ValueTween : MonoBehaviour
    {
        [SerializeField] private float startValue;
        [SerializeField] private float endValue;
        [SerializeField] private float duration;
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private UnityEvent<float> OnTween;
        [SerializeField] private UnityEvent OnComplete;

        public void StartTween()
        {
            DOTween.To(SetValue, startValue, endValue, duration).SetEase(curve).OnComplete(()=>
            {
                OnComplete?.Invoke();
            });
        }

        private void SetValue(float value)
        {
            OnTween?.Invoke(value);
        }
    }
}