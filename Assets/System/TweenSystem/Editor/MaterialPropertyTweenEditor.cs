using UnityEditor;
using UnityEngine;

namespace RedDevil.Tween
{
    [CustomEditor(typeof(MaterialPropertyTween))]

    public class MaterialPropertyTweenEditor : Editor
    {
        private MaterialPropertyTween materialProperty;
        private int id;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(!Application.isPlaying) return;
            id = EditorGUILayout.IntField("tween ID", id);
            if (GUILayout.Button("Start"))
            {
                materialProperty = (MaterialPropertyTween) target;
                materialProperty.StartTween(id);
            }

            if (GUILayout.Button("Reset"))
            {
                materialProperty = (MaterialPropertyTween) target;
                materialProperty.ResetTween(id);
            }
        }
    }
}