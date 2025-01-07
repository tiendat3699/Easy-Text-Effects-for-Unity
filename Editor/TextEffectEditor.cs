using UnityEditor;
using UnityEngine;
using static EasyTextEffects.Editor.EditorDocumentation.EditorDocumentation;

namespace EasyTextEffects.Editor
{
    [CustomEditor(typeof(TextEffect))]
    public class TextEffectEditor : UnityEditor.Editor
    {
        private string manualEffectName_;
        private string manualTagEffectName_;
        private bool debugButtonsVisible_;
        private bool documentationVisible_;
        private bool createEffectVisible_;
        private bool applyEffectVisible_;
        private bool controlEffectVisible_;

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
                if (myScript.text != null)
                    myScript.text.ForceMeshUpdate();
                myScript.UpdateStyleInfos();
            }

            BeginFoldBox("Debug Buttons", ref debugButtonsVisible_, IconType.Tool);
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

            EndFoldBox();

            BeginFoldBox("Creating Effects", ref createEffectVisible_);
            if (createEffectVisible_)
            {
                EditorGUILayout.BeginHorizontal();
                FileLink(
                    "Packages/com.qiaozhilei.easy-text-effects/Documentation/Documentation.md");
                UrlLink(
                    "https://github.com/LeiQiaoZhi/Easy-Text-Effect-for-Unity/blob/main/Documentation/Documentation.md#creating-effects");
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.LabelField(
                    @"Effects are ScriptableObjects that can be created in the project view. Right-click and select `Create/Easy Text Effect/[Text Effect Type]`. Since effects are assets, they can be shared between multiple `TextEffect` components, and changes to the effect will be reflected in all components.",
                    EditorStyles.wordWrappedLabel
                );
            }

            EndFoldBox();

            BeginFoldBox("Applying Effects", ref applyEffectVisible_);
            if (applyEffectVisible_)
            {
                EditorGUILayout.BeginHorizontal();
                FileLink(
                    "Packages/com.qiaozhilei.easy-text-effects/Documentation/Documentation.md");
                UrlLink(
                    "https://github.com/LeiQiaoZhi/Easy-Text-Effect-for-Unity/blob/main/Documentation/Documentation.md#applying-effects");
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.LabelField(
                    @"There are 2 effect lists:
1. Tag Effects: Effects that are applied to the text based on rich text tags.
2. Global Effects: Effects that are applied to every character in the text.

Global effects are very easy to apply, just add an element to the list and drag the effect to the Effect field.
- The option overrideTagEffects determines whether a global effect override tag effects or not.

Tag effects are applied by adding a rich text tag to the text. The format is <link=effectName>text</link>. The effectName should match the Effect Name of the effect.
- When adding multiple tag effects, the format is <link=effectName1+effectName2>text</link>. Don't include ""+"" in effect names for this reason.",
                    EditorStyles.wordWrappedLabel
                );
            }
            EndFoldBox();
            
            BeginFoldBox("Controlling Effects", ref controlEffectVisible_);
            if (controlEffectVisible_)
            {
                EditorGUILayout.BeginHorizontal();
                FileLink(
                    "Packages/com.qiaozhilei.easy-text-effects/Documentation/Documentation.md");
                UrlLink(
                    "https://github.com/LeiQiaoZhi/Easy-Text-Effect-for-Unity/blob/main/Documentation/Documentation.md#controlling-effects");
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.LabelField(
                    @"Every element of an effect list has a `Trigger When` field, which determines when the effect is triggered. 
- `On Start`: The effect will start when the text is enabled.
- `Manual`: The effect will start only when a script tells it to.
   - `StartAllManualEffects()`: start all manual effects in the global list.
   - `StartManualEffects(string effectName)`: start the manual effect with the given name in the global list.
   - `StartManualTagEffects()`: start all manual effects in the tag list.
   - `StartManualTagEffects(string effectName)`: start the manual effect with the given name in the tag list.

There are some debug buttons to help you test manual effects in the editor.",
                    EditorStyles.wordWrappedLabel
                );
            }
            EndFoldBox();
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