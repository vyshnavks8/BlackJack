using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace RedDevil.Tween
{
    [Serializable]
    public class UIMaterialTweenData
    {
        public enum MaterialTweenProperty
        {
            materialInt,
            materialFloat,
            materialColor,
            materialEmission
        }

        public string propertyID;
        public Material material;
        public MaterialTweenProperty property;
        public int tweenID;
        public float duration;
        public float setValue;
        public float resetValue;
        public Color setColor;
        public Color resetColor;
        [Header("Enable && Disable")] public float enableValue;
        public float disableValue;

        [Header("Auto Reset")] public bool autoReset;
        public float resetTimer;
        public int valueID { get; private set; } = -100;
        private Color GetColor() => material.GetColor(valueID);
        private Color GetEmissionColor() => emissionColor;
        private float GetFloat() => floatValue;

        private int GetInt() => intValue;
        private Color emissionColor;
        private int intValue;
        private float floatValue;

        public void InitPropertyValue()
        {
            valueID = Shader.PropertyToID(propertyID);
        }

        public void SetEnableValue()
        {
            Tween(enableValue, resetColor, 0);
        }

        public void SetDisableValue()
        {
            Tween(disableValue, resetColor, 0);
        }
        
        public void StartTween()
        {
            if (valueID == -100) return;
            Tween(setValue, setColor, duration);
        }


        public void ResetTween()
        {
            if (valueID == -100) return;
            Tween(resetValue, resetColor, duration);
        }

        private void Tween(float value, Color color, float dur)
        {
            switch (property)
            {
                case MaterialTweenProperty.materialInt:
                    DOTween.To(GetInt, SetMaterialInt, (int)value, dur).OnComplete(() => { intValue = (int)value; });
                    break;
                case MaterialTweenProperty.materialFloat:
                    DOTween.To(SetMaterialFloat, GetFloat(), value, dur).OnComplete(() => { floatValue = value; });
                    break;
                case MaterialTweenProperty.materialColor:
                    DOTween.To(GetColor, SetMaterialColor, color, dur);
                    break;
                case MaterialTweenProperty.materialEmission:

                    DOTween.To(GetEmissionColor, SetMaterialColor, color * value, dur)
                        .OnComplete(() => { emissionColor = color * value; });
                    break;
            }
        }
        private void SetMaterialColor(Color color)
        {
            material.SetColor(valueID, color);
        }

        private void SetMaterialFloat(float tweenValue)
        {
            material.SetFloat(valueID, tweenValue);
        }

        private void SetMaterialInt(int tweenValue)
        {
            material.SetInt(valueID, tweenValue);
        }
    }
}