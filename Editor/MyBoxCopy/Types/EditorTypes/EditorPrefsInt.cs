using System;
using UnityEditor;

#if UNITY_EDITOR
namespace EasyTextEffects.Editor.MyBoxCopy.Types.EditorTypes
{
	[Serializable]
	public class EditorPrefsInt : EditorPrefsType
	{
		public int Value
		{
			get => (int)EditorPrefs.GetFloat(Key, DefaultValue);
			set => EditorPrefs.SetFloat(Key, value);
		}

		public int DefaultValue;

		public static EditorPrefsInt WithKey(string key, int defaultValue = 0) => new EditorPrefsInt(key, defaultValue);
		
		public EditorPrefsInt(string key, int defaultValue = 0)
		{
			Key = key;
			DefaultValue = defaultValue;
		}
	}
}
#endif