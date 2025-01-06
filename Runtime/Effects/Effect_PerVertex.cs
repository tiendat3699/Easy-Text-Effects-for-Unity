using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace EasyTextEffects.Effects
{
    [CreateAssetMenu(fileName = "PerVertex", menuName = "Easy Text Effects/Per Vertex")]
    public class Effect_PerVertex : TextEffect_Trigger
    {
        [Space(10)] public List<TextEffect_Trigger> topLeftEffects = new List<TextEffect_Trigger>();
        [Space(10)] public List<TextEffect_Trigger> topRightEffects = new List<TextEffect_Trigger>();
        [Space(10)] public List<TextEffect_Trigger> bottomLeftEffects = new List<TextEffect_Trigger>();
        [Space(10)] public List<TextEffect_Trigger> bottomRightEffects = new List<TextEffect_Trigger>();

        private void OnValidate()
        {
            if (topLeftEffects.Contains(this))
            {
                Debug.LogError("Per Vertex effect can't contain itself");
                topLeftEffects.Remove(this);
            }

            if (topRightEffects.Contains(this))
            {
                Debug.LogError("Per Vertex effect can't contain itself");
                topRightEffects.Remove(this);
            }

            if (bottomLeftEffects.Contains(this))
            {
                Debug.LogError("Per Vertex effect can't contain itself");
                bottomLeftEffects.Remove(this);
            }

            if (bottomRightEffects.Contains(this))
            {
                Debug.LogError("Per Vertex effect can't contain itself");
                bottomRightEffects.Remove(this);
            }
        }

        public override void ApplyEffect(TMP_TextInfo _textInfo, int _charIndex, int _startVertex = 0,
            int _endVertex = 3)
        {
            if (!CheckCanApplyEffect(_charIndex)) return;

            topLeftEffects.ForEach(_effect => _effect?.ApplyEffect(_textInfo, _charIndex, 1, 1));
            topRightEffects.ForEach(_effect => _effect?.ApplyEffect(_textInfo, _charIndex, 2, 2));
            bottomLeftEffects.ForEach(_effect => _effect?.ApplyEffect(_textInfo, _charIndex, 0, 0));
            bottomRightEffects.ForEach(_effect => _effect?.ApplyEffect(_textInfo, _charIndex, 3, 3));
        }

        public override void StartEffect()
        {
            base.StartEffect();
            var allEffects = new List<List<TextEffect_Trigger>> 
            {
                topLeftEffects,
                topRightEffects,
                bottomLeftEffects,
                bottomRightEffects
            };

            foreach (var effects in allEffects)
            {
                foreach (TextEffect_Trigger effect in effects)
                {
                    if (!effect) continue;
                    effect.startCharIndex = startCharIndex;
                    effect.charLength = charLength;
                    effect.StartEffect();
                }
            }

        }

        public override void StopEffect()
        {
            base.StopEffect();

            topLeftEffects.ForEach(_effect => _effect?.StopEffect());
            topRightEffects.ForEach(_effect => _effect?.StopEffect());
            bottomLeftEffects.ForEach(_effect => _effect?.StopEffect());
            bottomRightEffects.ForEach(_effect => _effect?.StopEffect());
        }

        public override TextEffect_Trigger Instantiate()
        {
            Effect_PerVertex instance = Instantiate(this);
            instance.topLeftEffects = topLeftEffects.Select(_effect => _effect?.Instantiate()).ToList();
            instance.topRightEffects = topRightEffects.Select(_effect => _effect?.Instantiate()).ToList();
            instance.bottomLeftEffects = bottomLeftEffects.Select(_effect => _effect?.Instantiate()).ToList();
            instance.bottomRightEffects = bottomRightEffects.Select(_effect => _effect?.Instantiate()).ToList();
            return instance;
        }
    }
}