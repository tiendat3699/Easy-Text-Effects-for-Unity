using System.Collections.Generic;
using UnityEngine;

namespace Text_Effect
{
    [CreateAssetMenu(fileName = "TextEffectsPreset", menuName = "Text Effect/Text Effects Preset")]
    public class TextEffectsPreset : ScriptableObject
    {
        public List<TextEffect_Persistent> effects;

        public List<TextEffect_Persistent> GetEffectsByName(string _effectName)
        {
            var results = new List<TextEffect_Persistent>();
            var effectNames = _effectName.Split("+");

            foreach (var effectName in effectNames)
            {
                TextEffect_Persistent result = effects.Find(_effect => _effect.effectName == effectName);
                if (result == null)
                {
                    Debug.LogError("Effect not found: " + effectName);
                }
                else
                {
                    results.Add(result);
                }
            }
            return results;
        }
    }
}