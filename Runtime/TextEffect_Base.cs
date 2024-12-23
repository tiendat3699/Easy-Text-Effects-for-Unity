using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Text_Effect
{
    public abstract class TextEffect_Base : ScriptableObject
    {
        public string effectName;
        
        [HideInInspector] public int startCharIndex;
        [HideInInspector] public int charLength;

        public abstract void ApplyEffect(TMP_TextInfo _textInfo, int _charIndex);

        protected Vector3 CharCenter(TMP_CharacterInfo _charInfo, Vector3[] _verts)
        {
            
            var charBegin = _charInfo.vertexIndex;
            var charEnd = charBegin + 2;
            Vector3 center = (_verts[charBegin] + _verts[charEnd]) / 2;
            //
            // if (_charInfo.vertexIndex == 0)
            // {
            //     Debug.Log($"{center}");
            // }
            return center;
        }
    }
}