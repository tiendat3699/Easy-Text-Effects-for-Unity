using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace EasyTextEffects.Effects
{
    [CreateAssetMenu(fileName = "Composite", menuName = "Easy Text Effects/Composite")]
    public class Effect_Composite : TextEffect_Trigger
    {
        public List<TextEffect_Trigger> effects = new List<TextEffect_Trigger>();

        private void OnValidate()
        {
            if (effects.Contains(this))
            {
                Debug.LogError("Composite effect can't contain itself");
                effects.Remove(this);
            }
        }

        public override void ApplyEffect(TMP_TextInfo _textInfo, int _charIndex, int _startVertex = 0, int _endVertex = 3)
        {
            if (!CheckCanApplyEffect(_charIndex)) return;
            
            foreach (TextEffect_Trigger effect in effects)
            {
                effect.ApplyEffect(_textInfo, _charIndex, _startVertex, _endVertex);
            }
        }

        public override void StartEffect()
        {
            base.StartEffect();
            
            foreach (TextEffect_Trigger effect in effects)
            {
                effect.startCharIndex = startCharIndex;
                effect.charLength = charLength;
                effect.StartEffect();
            }
        }

        public override void StopEffect()
        {
            base.StopEffect();
            
            foreach (TextEffect_Trigger effect in effects)
            {
                effect.StopEffect();
            }
        }

        public override TextEffect_Trigger Instantiate()
        {
            Effect_Composite instance = Instantiate(this);
            instance.effects = new List<TextEffect_Trigger>();
            foreach (TextEffect_Trigger effect in effects)
            {
                instance.effects.Add(effect.Instantiate());
            }
            return instance;
        }
    }
}