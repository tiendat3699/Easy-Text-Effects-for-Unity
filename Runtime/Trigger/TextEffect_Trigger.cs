using MyBox;
using UnityEngine;
using UnityEngine.Serialization;

namespace EasyTextEffects
{
    public abstract class TextEffect_Trigger : TextEffect_Base
    {
        public enum AnimationType
        {
            Loop,
            LoopFixedDuration,
            PingPong,
            OneTime
        }

        [Space(10)] [Tooltip("Controls the warp modes of the easing curve.")]
        public AnimationType animationType = AnimationType.OneTime;

        [ConditionalField(nameof(animationType), false, AnimationType.LoopFixedDuration)]
        public float fixedDuration;

        [Space(10)] [FormerlySerializedAs("duration")]
        public float durationPerChar;

        public bool noDelayForChars;
        public float timeBetweenChars = 0.1f;
        public bool reverseCharOrder;

        [FormerlySerializedAs("curve")] public AnimationCurve easingCurve = AnimationCurve.Linear(0, 0, 1, 1);

        protected float startTime;
        protected bool started;

        protected bool CheckCanApplyEffect(int _charIndex)
        {
            return started && _charIndex >= startCharIndex && _charIndex < startCharIndex + charLength;
        }

        public virtual void StartEffect()
        {
            started = true;
            startTime = TimeUtil.GetTime();

            if (animationType == AnimationType.OneTime || animationType == AnimationType.LoopFixedDuration)
            {
                easingCurve.preWrapMode = WrapMode.Clamp;
                easingCurve.postWrapMode = WrapMode.Clamp;
            }
            else if (animationType == AnimationType.Loop)
            {
                easingCurve.preWrapMode = noDelayForChars ? WrapMode.Loop : WrapMode.Clamp;
                easingCurve.postWrapMode = WrapMode.Loop;
            }
            else if (animationType == AnimationType.PingPong)
            {
                easingCurve.preWrapMode = noDelayForChars ? WrapMode.PingPong : WrapMode.Clamp;
                easingCurve.postWrapMode = WrapMode.PingPong;
            }
        }

        public virtual void StopEffect()
        {
            started = false;
        }

        protected float Interpolate(float _start, float _end, int _charIndex)
        {
            var time = GetTimeForChar(_charIndex);
            return Mathf.Lerp(_start, _end, easingCurve.Evaluate(time / durationPerChar));
        }

        protected Vector2 Interpolate(Vector2 _start, Vector2 _end, int _charIndex)
        {
            var time = GetTimeForChar(_charIndex);
            return Vector2.Lerp(_start, _end, easingCurve.Evaluate(time / durationPerChar));
        }

        protected Color Interpolate(Color _start, Color _end, int _charIndex)
        {
            var time = GetTimeForChar(_charIndex);
            return Color.Lerp(_start, _end, easingCurve.Evaluate(time / durationPerChar));
        }

        private float GetTimeForChar(int _charIndex)
        {
            var time = TimeUtil.GetTime();
            if (animationType == AnimationType.LoopFixedDuration && time - startTime > fixedDuration)
                startTime += fixedDuration;

            var charOrder = _charIndex - startCharIndex;
            if (reverseCharOrder)
                charOrder = charLength - charOrder - 1;
            var charStartTime = startTime + timeBetweenChars * charOrder;
            return time - charStartTime;
        }
        
        public virtual TextEffect_Trigger Instantiate()
        {
            return Instantiate(this);
        }
    }
}