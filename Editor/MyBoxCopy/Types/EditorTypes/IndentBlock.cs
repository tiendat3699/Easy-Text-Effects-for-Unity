#if UNITY_EDITOR
using System;
using UnityEditor;

namespace EasyTextEffects.Editor.MyBoxCopy.Types.EditorTypes
{
	public class IndentBlock : IDisposable
	{
		public IndentBlock()
		{
			EditorGUI.indentLevel++;
		}

		public void Dispose()
		{
			EditorGUI.indentLevel--;
		}
	}
}
#endif