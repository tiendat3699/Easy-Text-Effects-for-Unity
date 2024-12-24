using TMPro;
using UnityEngine;

namespace EasyTextEffects
{
    [CreateAssetMenu(fileName = "Persistent_Wave", menuName = "Text Effect/Persistent/Wave")]
    public class Persistent_Wave : TextEffect_Persistent
    {
        public Vector2 direction = Vector2.up;
        public float amplitude = 5;
        public float frequency = 1;
        public float speed = 1;

        public override void ApplyEffect(TMP_TextInfo _textInfo, int _charIndex)
        {
            TMP_CharacterInfo charInfo = _textInfo.characterInfo[_charIndex];
            var materialIndex = charInfo.materialReferenceIndex;
            var verts = _textInfo.meshInfo[materialIndex].vertices;
            Vector2 offset =
                direction * (Mathf.Sin(TimeUtil.GetTime() * speed + _charIndex * frequency) * amplitude);
            for (var v = 0; v < 4; v++)
            {
                var vertexIndex = charInfo.vertexIndex + v;

                Vector3 originalPos = verts[vertexIndex];
                verts[vertexIndex] = originalPos + new Vector3(offset.x, offset.y, 0);
            }
        }
    }
}