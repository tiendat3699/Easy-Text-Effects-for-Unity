using System;
using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace EasyTextEffects
{
    [CreateAssetMenu(fileName = "Persistent_Rotate", menuName = "Text Effect/Persistent/Rotate")]
    public class Persistent_Rotate : TextEffect_Persistent
    {
        public enum RotateType
        {
            SinWave,
            Random,
            RandomStatic,
            AnimationCurve
        }

        public RotateType rotateType;

        public float maxAngle = 5;

        [ConditionalField("rotateType", false, RotateType.AnimationCurve, RotateType.SinWave)]
        public float speed = 1;

        [ConditionalField("rotateType", false, RotateType.RandomStatic)]
        public int seed;

        [ConditionalField("rotateType", false, RotateType.AnimationCurve)]
        public AnimationCurve angleAlongChar;


        public override void ApplyEffect(TMP_TextInfo _textInfo, int _charIndex)
        {
            TMP_CharacterInfo charInfo = _textInfo.characterInfo[_charIndex];
            var materialIndex = charInfo.materialReferenceIndex;
            var verts = _textInfo.meshInfo[materialIndex].vertices;
            Vector3 center = CharCenter(charInfo, verts);
            
            if (rotateType == RotateType.RandomStatic)
            {
                Random.InitState(seed + _charIndex);
            }

            var angle = rotateType switch
            {
                RotateType.SinWave => Mathf.Sin(TimeUtil.GetTime() * speed) * maxAngle,
                RotateType.Random => Random.Range(-maxAngle, maxAngle),
                RotateType.RandomStatic => Random.Range(-maxAngle, maxAngle),
                RotateType.AnimationCurve => angleAlongChar.Evaluate(
                    (float)(_charIndex - startCharIndex) / charLength + TimeUtil.GetTime() * speed) * maxAngle,
                _ => throw new ArgumentOutOfRangeException()
            };

            for (var v = 0; v < 4; v++)
            {
                var vertexIndex = charInfo.vertexIndex + v;

                Vector3 originalPos = verts[vertexIndex];
                Vector3 fromCenter = originalPos - center;


                Vector3 rotated = Quaternion.Euler(0, 0, angle) * fromCenter;
                verts[vertexIndex] = center + rotated;
            }
        }
    }
}