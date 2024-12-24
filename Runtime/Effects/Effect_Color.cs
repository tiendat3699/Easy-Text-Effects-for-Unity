using MyBox;
using TMPro;
using UnityEngine;

namespace EasyTextEffects.Effects
{
    [CreateAssetMenu(fileName = "Effect_Color", menuName = "Easy Text Effects/Color")]
    public class Effect_Color : TextEffect_Trigger
    {
        public enum ColorType
        {
            Gradient,
            BetweenTwoColors,
            OnlyAlpha,
            ColorToOriginal,
        }

        public enum GradientOrientation
        {
            Horizontal,
            HorizontalPerCharacter,
            Vertical,
        }

        public ColorType colorType;

        [ConditionalField(nameof(colorType), false, ColorType.BetweenTwoColors, ColorType.ColorToOriginal)]
        public Color startColor = Color.white;

        [ConditionalField(nameof(colorType), false, ColorType.BetweenTwoColors)]
        public Color endColor = Color.white;


        [ConditionalField(nameof(colorType), false, ColorType.Gradient)]
        public Gradient gradient;

        [ConditionalField(nameof(colorType), false, ColorType.Gradient)]
        public GradientOrientation orientation;

        [ConditionalField(nameof(orientation), false, GradientOrientation.HorizontalPerCharacter,
            GradientOrientation.Vertical)]
        public int stride = 10;


        [ConditionalField(nameof(colorType), false, ColorType.OnlyAlpha)] [Range(0, 1)]
        public float startAlpha = 0;

        [ConditionalField(nameof(colorType), false, ColorType.OnlyAlpha)] [Range(0, 1)]
        public float endAlpha = 1;

        public override void ApplyEffect(TMP_TextInfo _textInfo, int _charIndex)
        {
            if (!CheckCanApplyEffect(_charIndex)) return;

            TMP_CharacterInfo charInfo = _textInfo.characterInfo[_charIndex];
            var materialIndex = charInfo.materialReferenceIndex;

            for (var v = 0; v < 4; v++)
            {
                var vertexIndex = charInfo.vertexIndex + v;
                Color color = _textInfo.meshInfo[materialIndex].colors32[vertexIndex];
                if (colorType == ColorType.Gradient)
                {
                    if (orientation == GradientOrientation.Horizontal)
                    {
                        var t = Interpolate(0, 1, _charIndex);
                        color = gradient.Evaluate(t);
                    }

                    if (orientation == GradientOrientation.HorizontalPerCharacter)
                    {
                        var start = Interpolate(0, 1, _charIndex);
                        var end = Interpolate(0, 1, _charIndex + stride);
                        var t = v == 0 || v == 1 ? start : end;
                        color = gradient.Evaluate(t);
                    }

                    if (orientation == GradientOrientation.Vertical)
                    {
                        var start = Interpolate(0, 1, _charIndex);
                        var end = Interpolate(0, 1, _charIndex + stride);
                        var t = v == 1 || v == 2 ? start : end;
                        color = gradient.Evaluate(t);
                    }
                }
                else if (colorType == ColorType.BetweenTwoColors)
                {
                    color = Interpolate(startColor, endColor, _charIndex);
                }
                else if (colorType == ColorType.OnlyAlpha)
                {
                    color.a = Interpolate(startAlpha, endAlpha, _charIndex);
                }
                else if (colorType == ColorType.ColorToOriginal)
                {
                    color = Interpolate(startColor, color, _charIndex);
                }

                _textInfo.meshInfo[materialIndex].colors32[vertexIndex] = color;
            }
        }
    }
}