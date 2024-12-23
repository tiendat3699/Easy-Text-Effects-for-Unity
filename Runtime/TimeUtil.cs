using UnityEditor;
using UnityEngine;

namespace Text_Effect
{
    public static class TimeUtil
    {
        private static float editorStartTime = -1;

        public static float GetTime()
        {
            if (Application.isPlaying)
            {
                return Time.time;
            }
            else
            {
#if UNITY_EDITOR
                if (editorStartTime < 0)
                {
                    editorStartTime = (float)EditorApplication.timeSinceStartup; // Capture when this started
                }

                // Normalize time to start from 0 in the editor
                return (float)(EditorApplication.timeSinceStartup - editorStartTime);
#else
            return 0f; // Fallback if outside editor
#endif
            }
        }
    }
}