using DG.Tweening;
using UnityEngine;

namespace RedDevil.Tween
{
    public class TranslateTween : MonoBehaviour
    {
        [SerializeField] private bool startOnAwake;
        [SerializeField] private float duration = 3;
        [SerializeField] private float startDelay;
        [SerializeField] private Ease ease = Ease.Linear;
        [SerializeField] private Vector3 location;
        private Vector3 localPosition;

        private void Awake()
        {
            localPosition = transform.localPosition;
            if (startOnAwake)
            {
                Translate();
            }
        }

        public void Translate()
        {
            transform.DOLocalMove(location, duration).SetDelay(startDelay).SetEase(ease);
        }

        public void ResetTranslate()
        {
            transform.DOLocalMove(localPosition, duration).SetEase(ease).OnComplete(OnReset);
        }

        private void OnReset()
        {
            /*OnFail?.Invoke();*/
        }
    }
}