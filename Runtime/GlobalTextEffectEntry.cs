namespace Text_Effect
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
        public TextEffect_Trigger effect;
    }

    [System.Serializable]
    public class GlobalTextEffectEntry : TextEffectEntry
    {
        public bool overrideTagEffects;
    }
}