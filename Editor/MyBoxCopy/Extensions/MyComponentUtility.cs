using JetBrains.Annotations;
using UnityEngine;

namespace EasyTextEffects.Editor.MyBoxCopy.Extensions
{
	[PublicAPI]
	public static class MyComponentUtility
	{
		public static void MoveComponentInspectorToTop(Component component)
		{
#if UNITY_EDITOR
			while (UnityEditorInternal.ComponentUtility.MoveComponentUp(component)){}
#endif
		}

		public static void MoveComponentInspectorToBottom(Component component)
		{
#if UNITY_EDITOR
			while (UnityEditorInternal.ComponentUtility.MoveComponentDown(component)){}
#endif
		}
	}
}