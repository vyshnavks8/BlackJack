using UnityEditor;
using UnityEngine;

namespace RedDevil.Tween
{
    [CustomEditor(typeof(RotationTween)),CanEditMultipleObjects]
    public class RotationTweenEditor : Editor
    {
        private RotationTween rotationTween;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(!Application.isPlaying) return;
            rotationTween = target as RotationTween;
            if (rotationTween == null) return;
            if (GUILayout.Button("Rotate"))
            {
                rotationTween.Rotate();
            }
        }
    }[CustomEditor(typeof(TranslateTween)),CanEditMultipleObjects]
    public class TranslateTweenEditor : Editor
    {
        private TranslateTween translateTween;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(!Application.isPlaying) return;
            translateTween = target as TranslateTween;
            if (translateTween == null) return;
            if (GUILayout.Button("Translate"))
            {
                translateTween.Translate();
            } if (GUILayout.Button("Reset"))
            {
                translateTween.ResetTranslate();
            }
        }
    }
}