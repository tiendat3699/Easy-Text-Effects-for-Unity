using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace EasyTextEffects
{
    [CreateAssetMenu(fileName = "Trigger_Scale", menuName = "Text Effect/Trigger/Scale")]
    public class Trigger_Scale : TextEffect_Trigger
    {
        public float startScale = 0;
        public float endScale = 1;

        public override void ApplyEffect(TMP_TextInfo _textInfo, int _charIndex)
        {
            if (!CheckCanApplyEffect(_charIndex)) return;

            TMP_CharacterInfo charInfo = _textInfo.characterInfo[_charIndex];
            var materialIndex = charInfo.materialReferenceIndex;
            var verts = _textInfo.meshInfo[materialIndex].vertices;
            Vector3 center = CharCenter(charInfo, verts);

            var scale = Interpolate(startScale, endScale, _charIndex);

            for (var v = 0; v < 4; v++)
            {
                var vertexIndex = charInfo.vertexIndex + v;
                Vector3 fromCenter = verts[vertexIndex] - center;
                verts[vertexIndex] = center + fromCenter * scale;
            }
        }
    }
}