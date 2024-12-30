#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EasyTextEffects.Editor.EditorDocumentation
{
    [CustomPropertyDrawer(typeof(FoldBoxAttribute))]
    public class FoldBoxAttributeDrawer : PropertyDrawer
    {
        private bool foldoutState = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label);

            var foldBoxAttribute = attribute as FoldBoxAttribute;
            if (foldBoxAttribute == null) return;

            EditorDocumentation.BeginFoldBox(foldBoxAttribute.Title, ref foldoutState);
            if (foldoutState)
            {
                for (int i = 0; i < foldBoxAttribute.Content.Length; i++)
                {
                    FoldBoxAttribute.ContentType contentType =
                        (foldBoxAttribute.ContentTypes[Mathf.Min(i, foldBoxAttribute.ContentTypes.Length - 1)]);
                    if (contentType == FoldBoxAttribute.ContentType.Text)
                    {
                        EditorGUILayout.LabelField(foldBoxAttribute.Content[i], EditorStyles.wordWrappedLabel);
                    }
                    else
                    {
                        var param = foldBoxAttribute.Content[i].Split(",");
                        Debug.Log(string.Join("--", param));
                        var image = AssetDatabase.LoadAssetAtPath<Texture2D>(param[0]);
                        if (image != null)
                        {
                            if (param.Length == 1)
                                EditorGUILayout.LabelField(new GUIContent(image), GUILayout.Height(200));
                            else if (param.Length == 2)
                            {
                                var width = int.Parse(param[1]);
                                var height = image.height * width / image.width;
                                EditorGUILayout.LabelField(new GUIContent(image), GUILayout.Width(width), GUILayout.Height(height));
                            }
                            else if (param.Length == 3)
                                EditorGUILayout.LabelField(new GUIContent(image), GUILayout.Width(int.Parse(param[1])),
                                    GUILayout.Height(int.Parse(param[2])));
                        }
                        else
                        {
                            EditorGUILayout.LabelField(foldBoxAttribute.Content[i], EditorStyles.boldLabel);
                        }
                    }
                }
            }

            EditorDocumentation.EndFoldBox();
        }
    }
}
#endif