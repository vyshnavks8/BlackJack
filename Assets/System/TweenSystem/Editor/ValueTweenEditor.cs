using UnityEditor;
using UnityEngine;

namespace RedDevil.Tween
{
    [CustomEditor(typeof(ValueTween)),CanEditMultipleObjects]
    public class ValueTweenEditor : Editor
    {
        private ValueTween rotationTween;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(!Application.isPlaying) return;
            rotationTween = target as ValueTween;
            if (rotationTween == null) return;
            if (GUILayout.Button("Start Tween"))
            {
                rotationTween.StartTween();
            }
        }
    }
}