using EasyTextEffects.Effects;
using UnityEditor;

namespace EasyTextEffects.Editor
{
    [CustomEditor(typeof(Effect_PerVertex))]
    public class PerVertexEffectEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var myScript = (Effect_PerVertex)target;

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("effectTag"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("topLeftEffects"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("topRightEffects"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bottomLeftEffects"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("bottomRightEffects"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}