using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace RedDevil.Tween
{
    public class UIImageTween : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Color startValue;
        [SerializeField] private Color endValue;
        [SerializeField] private float duration;
        [SerializeField] private AnimationCurve curve;

        public void StartColorTween()
        {
            DOTween.To(GetColor, SetColor, endValue, duration).SetEase(curve);
        }

        private Color GetColor()
        {
            return startValue;
        }

        private void SetColor(Color value)
        {
            image.color = value;
        }
    }
}