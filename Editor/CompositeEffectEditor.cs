using EasyTextEffects.Effects;
using UnityEditor;

namespace EasyTextEffects.Editor
{
    [CustomEditor(typeof(Effect_Composite))]
    public class CompositeEffectEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var myScript = (Effect_Composite)target;
            
            // draw the list of effects 
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("effectName"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("effects"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}