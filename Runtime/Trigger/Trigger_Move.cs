using MyBox;
using TMPro;
using UnityEngine;

namespace Text_Effect
{
    [CreateAssetMenu(fileName = "Trigger_Move", menuName = "Text Effect/Trigger/Move")]
    public class Trigger_Move : TextEffect_Trigger
    {
        public Vector2 startOffset = -Vector2.up;
        public Vector2 endOffset;

        public override void ApplyEffect(TMP_TextInfo _textInfo, int _charIndex)
        {
            if (!CheckCanApplyEffect(_charIndex)) return;

            TMP_CharacterInfo charInfo = _textInfo.characterInfo[_charIndex];
            var materialIndex = charInfo.materialReferenceIndex;
            var verts = _textInfo.meshInfo[materialIndex].vertices;

            for (var v = 0; v < 4; v++)
            {
                var vertexIndex = charInfo.vertexIndex + v;
                Vector2 offset = Interpolate(startOffset, endOffset, _charIndex);
                verts[vertexIndex] += new Vector3(offset.x, offset.y, 0);
            }
        }
    }
}