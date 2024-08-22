using UnityEditor;
using UnityEngine;

namespace RedDevil.Tween
{
    [CustomEditor(typeof(UIMaterialTween))]

    public class UIMaterialPropertyTweenEditor : Editor
    {
        private UIMaterialTween materialProperty;
        private int id;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(!Application.isPlaying) return;
            id = EditorGUILayout.IntField("tween ID", id);
            if (GUILayout.Button("Start"))
            {
                materialProperty = (UIMaterialTween) target;
                materialProperty.StartTween(id);
            }

            if (GUILayout.Button("Reset"))
            {
                materialProperty = (UIMaterialTween) target;
                materialProperty.ResetTween(id);
            }
        }
    }
}