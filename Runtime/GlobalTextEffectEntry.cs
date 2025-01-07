using EasyTextEffects.Effects;

namespace EasyTextEffects
{
    [System.Serializable]
    public class TextEffectEntry
    {
        public enum TriggerWhen
        {
            OnStart,
            Manual,
        }

        public TriggerWhen triggerWhen;
        public TextEffectInstance effect;
    }

    [System.Serializable]
    public class GlobalTextEffectEntry : TextEffectEntry
    {
        public bool overrideTagEffects;
    }
}