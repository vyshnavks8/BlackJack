using UnityEditor;
using UnityEngine;

namespace RedDevil.Tween
{
    [CustomEditor(typeof(UIImageTween)),CanEditMultipleObjects]
    public class UIImageTweenEditor : Editor
    {
        private UIImageTween rotationTween;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(!Application.isPlaying) return;
            rotationTween = target as UIImageTween;
            if (rotationTween == null) return;
            if (GUILayout.Button("Color Tween"))
            {
                rotationTween.StartColorTween();
            }
        }
    }
}