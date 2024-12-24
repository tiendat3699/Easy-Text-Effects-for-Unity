using TMPro;
using UnityEngine;

namespace EasyTextEffects.Effects
{
    [CreateAssetMenu(fileName = "Effect_Rotate", menuName = "Easy Text Effects/Rotate")]
    public class Effect_Rotate : TextEffect_Trigger
    {
        [Range(-360, 360)] public float startAngle = 90;
        [Range(-360, 360)] public float endAngle = 0;

        public override void ApplyEffect(TMP_TextInfo _textInfo, int _charIndex)
        {
            if (!CheckCanApplyEffect(_charIndex)) return;

            TMP_CharacterInfo charInfo = _textInfo.characterInfo[_charIndex];
            var materialIndex = charInfo.materialReferenceIndex;
            var verts = _textInfo.meshInfo[materialIndex].vertices;

            var angle = Interpolate(startAngle, endAngle, _charIndex);
            Vector3 center = CharCenter(charInfo, verts);
            
            for (var v = 0; v < 4; v++)
            {
                var vertexIndex = charInfo.vertexIndex + v;
                Vector3 fromCenter = verts[vertexIndex] - center;
                verts[vertexIndex] = center + Quaternion.Euler(0, 0, angle) * fromCenter;
            }
        }
    }
}