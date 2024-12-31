using UnityEditor;
using UnityEngine;

namespace EasyTextEffects.Editor
{
    [CustomEditor(typeof(TextEffect))]
    public class TextEffectEditor : UnityEditor.Editor
    {
        private string manualEffectName_;
        private string manualTagEffectName_;
        private bool debugButtonsVisible_;
        private bool documentationVisible_;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var myScript = (TextEffect)target;

            GUILayout.Space(10);

            var buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 14;
            buttonStyle.fixedHeight = 30;
            buttonStyle.fontStyle = FontStyle.Bold;

            if (GUILayout.Button("REFRESH", buttonStyle))
            {
                myScript.text.ForceMeshUpdate();
                myScript.UpdateStyleInfos();
            }

            EditorDocumentation.EditorDocumentation.BeginFoldBox("Debug Buttons", ref debugButtonsVisible_, EditorDocumentation.EditorDocumentation.IconType.Tool);
            if (debugButtonsVisible_)
            {
                GUILayout.BeginHorizontal();
                manualTagEffectName_ = EditorGUILayout.TextField("Manual Tag Effect Name", manualTagEffectName_);
                if (GUILayout.Button("START"))
                {
                    myScript.StartManualEffect(manualTagEffectName_);
                }
                GUILayout.EndHorizontal();
            
                GUILayout.BeginHorizontal();
                manualEffectName_ = EditorGUILayout.TextField("Manual Global Effect Name", manualEffectName_);
                if (GUILayout.Button("START"))
                {
                    myScript.StartManualTagEffect(manualEffectName_);
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(10);
            
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("START Tag Manual Effects", buttonStyle))
                {
                    myScript.StartManualTagEffects();
                }

                if (GUILayout.Button("STOP", buttonStyle))
                {
                    myScript.StopManualTagEffects();
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("START Global Manual Effects", buttonStyle))
                {
                    myScript.StartManualEffects();
                }

                if (GUILayout.Button("STOP", buttonStyle))
                {
                    myScript.StopManualEffects();
                }
                GUILayout.EndHorizontal();
            }
            EditorDocumentation.EditorDocumentation.EndFoldBox();

            EditorDocumentation.EditorDocumentation.BeginFoldBox("Documentation", ref documentationVisible_);
            EditorDocumentation.EditorDocumentation.EndFoldBox();

        }

        private void DrawFoldoutHeader(string title, ref bool foldoutState)
        {
            // Create a horizontal layout for the foldout header
            EditorGUILayout.BeginHorizontal("box"); // Box for the header background
            GUILayout.Label(title, EditorStyles.boldLabel);

            // Right-aligned label for "(Click to expand)"
            GUILayout.FlexibleSpace();
            GUIStyle clickToExpandStyle = new GUIStyle(EditorStyles.label)
            {
                fontStyle = FontStyle.Italic,
                fontSize = 12
            };
            GUILayout.Label(foldoutState ? "(Click to collapse)" : "(Click to expand)", clickToExpandStyle);

            // Check for mouse click to toggle foldout state
            Rect headerRect = GUILayoutUtility.GetLastRect();
            if (Event.current.type == EventType.MouseDown && headerRect.Contains(Event.current.mousePosition))
            {
                foldoutState = !foldoutState; // Toggle foldout state
                Event.current.Use();
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}