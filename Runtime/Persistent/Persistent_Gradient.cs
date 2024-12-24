using MyBox;
using TMPro;
using UnityEngine;

namespace EasyTextEffects
{
    [CreateAssetMenu(fileName = "Persistent_Gradient", menuName = "Text Effect/Persistent/Gradient")]
    public class Persistent_Gradient : TextEffect_Persistent
    {
        public enum GradientOrientation
        {
            Horizontal,
            HorizontalAcrossTime,
            HorizontalPerCharacter,
            Vertical,
        }

        public GradientOrientation orientation;
        public Gradient gradient;

        [ConditionalField(nameof(orientation), false, GradientOrientation.HorizontalAcrossTime)]
        public float speed = 1;

        public override void ApplyEffect(TMP_TextInfo _textInfo, int _charIndex)
        {
            TMP_CharacterInfo charInfo = _textInfo.characterInfo[_charIndex];
            var materialIndex = charInfo.materialReferenceIndex;
            for (var v = 0; v < 4; v++)
            {
                var vertexIndex = charInfo.vertexIndex + v;
                Color color = EvaluateGradient(_charIndex, vertexIndex);
                _textInfo.meshInfo[materialIndex].colors32[vertexIndex] = color;
            }
        }

        private Color EvaluateGradient(int _c, int _v)
        {
            if (orientation == GradientOrientation.Horizontal)
            {
                return gradient.Evaluate((float)(_c - startCharIndex) / charLength);
            }
            else if (orientation == GradientOrientation.HorizontalPerCharacter)
            {
                var v = _v % 4;
                return gradient.Evaluate(v == 0 || v == 1 ? 0 : 1);
            }
            else if (orientation == GradientOrientation.HorizontalAcrossTime)
            {
                var t = TimeUtil.GetTime() * speed;
                t = (Mathf.FloorToInt(t) % 2 == 1) ? 1 - t % 1 : t % 1;
                return gradient.Evaluate(t);
            }
            else // Vertical
            {
                var v = _v % 4;
                return gradient.Evaluate(v == 1 || v == 2 ? 0 : 1);
            }
        }
    }
}