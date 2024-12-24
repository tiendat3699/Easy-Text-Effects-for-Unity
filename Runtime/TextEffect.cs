using System.Collections.Generic;
using System.Linq;
using MyBox;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Serialization;

namespace EasyTextEffects
{
    [ExecuteAlways]
    public class TextEffect : MonoBehaviour
    {
        public TMP_Text text;
        public TextEffectsPreset effectsPreset;
        public List<TextEffectEntry> tagEffects;
        [FormerlySerializedAs("effectsList")] public List<GlobalTextEffectEntry> globalEffects;
        [Range(1, 120)] public int updatesPerSecond = 30;

        public struct StyleInfo
        {
            public List<TextEffect_Persistent> effects;
        }

        private List<TextEffectEntry> onStartTagEffects_;
        private List<TextEffectEntry> manualTagEffects_;
        private List<GlobalTextEffectEntry> onStartEffects_;
        private List<GlobalTextEffectEntry> manualEffects_;
        private StyleInfo[] styleInfos_;
        private List<TextEffect_Trigger> entryEffectsCopied_;

        public void UpdateStyleInfos()
        {
            if (text == null || text.textInfo == null)
                return;
            TMP_TextInfo textInfo = text.textInfo;
            styleInfos_ = new StyleInfo[textInfo.characterCount];

            var styles = textInfo.linkInfo;

            // initialize effects list
            for (var i = 0; i < styleInfos_.Length; i++)
            {
                styleInfos_[i].effects = new List<TextEffect_Persistent>();
            }

            // copy global effects
            onStartEffects_ = new List<GlobalTextEffectEntry>();
            manualEffects_ = new List<GlobalTextEffectEntry>();
            globalEffects.ForEach(_entry =>
            {
                if (_entry.effect == null)
                    return;
                var effectEntry = new GlobalTextEffectEntry();
                effectEntry.effect = _entry.effect.Instantiate();
                effectEntry.effect.startCharIndex = 0;
                effectEntry.effect.charLength = textInfo.characterCount;
                effectEntry.overrideTagEffects = _entry.overrideTagEffects;
                if (_entry.triggerWhen == GlobalTextEffectEntry.TriggerWhen.OnStart)
                    onStartEffects_.Add(effectEntry);
                else
                    manualEffects_.Add(effectEntry);
            });

            // add effects to list
            onStartTagEffects_ = new List<TextEffectEntry>();
            manualTagEffects_ = new List<TextEffectEntry>();
            for (var i = 0; i < styles.Length; i++)
            {
                TMP_LinkInfo style = styles[i];
                if (style.GetLinkID() == string.Empty)
                    continue;

                // copy effects
                var effectTemplates = GetTagEffectsByName(style.GetLinkID());
                // Debug.Log($"style {i} -- {style.GetLinkID()} -- {effectTemplates.Count} effects");
                foreach (TextEffectEntry entry in effectTemplates)
                {
                    if (entry.effect == null)
                        continue;
                    var entryCopy = new TextEffectEntry();
                    entryCopy.effect = entry.effect.Instantiate();
                    entryCopy.effect.startCharIndex = style.linkTextfirstCharacterIndex;
                    entryCopy.effect.charLength = style.linkTextLength;
                    if (entry.triggerWhen == TextEffectEntry.TriggerWhen.OnStart)
                        onStartTagEffects_.Add(entryCopy);
                    else
                        manualTagEffects_.Add(entryCopy);
                }
            }

            StartOnStartEffects();
            text.fontMaterial.SetFloat("_UseTex", 0);
        }

        private List<TextEffectEntry> GetTagEffectsByName(string _effectName)
        {
            var results = new List<TextEffectEntry>();
            var effectNames = _effectName.Split("+");

            foreach (var effectName in effectNames)
            {
                TextEffectEntry result = tagEffects.Find(_entry => _entry.effect.effectName == effectName);
                if (result == null)
                {
                    Debug.LogWarning("Effect not found: " + effectName);
                }
                else
                {
                    results.Add(result);
                }
            }

            return results;
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (text == null)
                return;
            text.ForceMeshUpdate();
            UpdateStyleInfos();
#endif
        }

        private void OnEnable()
        {
#if UNITY_EDITOR
            EditorApplication.update += Update;
#endif
            if (text == null)
                return;
            text.ForceMeshUpdate();
            UpdateStyleInfos();
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            EditorApplication.update -= Update;
#endif
        }

        private void Start()
        {
            if (text == null)
                return;
            text.ForceMeshUpdate();
            UpdateStyleInfos();
        }

        private float nextUpdateTime_ = 0;

        public void Update()
        {
            if (!text)
                return;

            var time = TimeUtil.GetTime();
            if (time < nextUpdateTime_)
                return;
            nextUpdateTime_ = time + 1f / updatesPerSecond;

            text.ForceMeshUpdate();
            TMP_TextInfo textInfo = text.textInfo;

            for (var i = 0; i < textInfo.characterCount; i++)
            {
                TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
                if (!charInfo.isVisible)
                    continue;

                var capturedI = i;
                onStartEffects_.Where(_entry => !_entry.overrideTagEffects)
                    .ForEach(_entry => _entry.effect.ApplyEffect(textInfo, capturedI));
                manualEffects_.Where(_entry => !_entry.overrideTagEffects).ForEach(_entry =>
                    _entry.effect.ApplyEffect(textInfo, capturedI));

                onStartTagEffects_.ForEach(_entry => _entry.effect.ApplyEffect(textInfo, capturedI));
                manualTagEffects_.ForEach(_entry => _entry.effect.ApplyEffect(textInfo, capturedI));

                onStartEffects_.Where(_entry => _entry.overrideTagEffects).ForEach(_entry =>
                    _entry.effect.ApplyEffect(textInfo, capturedI));
                manualEffects_.Where(_entry => _entry.overrideTagEffects).ForEach(_entry =>
                    _entry.effect.ApplyEffect(textInfo, capturedI));
            }

            // apply changes and update mesh
            for (var i = 0; i < textInfo.meshInfo.Length; i++)
            {
                TMP_MeshInfo meshInfo = textInfo.meshInfo[i];

                meshInfo.mesh.colors32 = meshInfo.colors32;
                meshInfo.mesh.vertices = meshInfo.vertices;

                text.UpdateGeometry(meshInfo.mesh, i);
            }
        }

        public void StartOnStartEffects()
        {
            onStartEffects_.ForEach(_effect => _effect.effect.StartEffect());
            onStartTagEffects_.ForEach(_effect => _effect.effect.StartEffect());
        }

        public void StartManualEffects()
        {
            manualEffects_.ForEach(_effect => _effect.effect.StartEffect());
        }

        public void StopManualEffects()
        {
            manualEffects_.ForEach(_effect => _effect.effect.StopEffect());
        }

        public void StartManualTagEffects()
        {
            manualTagEffects_.ForEach(_effect => _effect.effect.StartEffect());
        }

        public void StopManualTagEffects()
        {
            manualTagEffects_.ForEach(_effect => _effect.effect.StopEffect());
        }

        public void StartManualEffect(string _effectName)
        {
            GlobalTextEffectEntry effectEntry = manualEffects_.Find(_entry => _entry.effect.effectName == _effectName);

            var names = manualEffects_.Select(_entry => _entry.effect.effectName).ToList();

            if (effectEntry != null)
                effectEntry.effect.StartEffect();
            else
            {
                Debug.LogWarning($"Effect {_effectName} not found");
                Debug.Log($"Available effects: {string.Join(", ", names)}");
            }
        }

        public void StartManualTagEffect(string _effectName)
        {
            TextEffectEntry effectEntry = manualTagEffects_.Find(_entry => _entry.effect.effectName == _effectName);

            var names = manualTagEffects_.Select(_entry => _entry.effect.effectName).ToList();

            if (effectEntry != null)
                effectEntry.effect.StartEffect();
            else
            {
                Debug.LogWarning($"Effect {_effectName} not found");
                Debug.Log($"Available effects: {string.Join(", ", names)}");
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (text == null || text.textInfo == null)
                return;
            Gizmos.color = Color.green;
            for (int i = 0; i < text.textInfo.characterCount; i++)
            {
                TMP_CharacterInfo charInfo = text.textInfo.characterInfo[i];
                if (!charInfo.isVisible)
                    continue;

                Vector3 botLeft = text.transform.TransformPoint(charInfo.bottomLeft);
                Vector3 topRight = text.transform.TransformPoint(charInfo.topRight);

                var verts = text.textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
                var first = verts[charInfo.vertexIndex + 0];
                var last = verts[charInfo.vertexIndex + 2];

                first = text.transform.TransformPoint(first);
                last = text.transform.TransformPoint(last);

                Gizmos.color = Color.red;
                // Gizmos.DrawWireSphere(first, 10);
                // Gizmos.DrawWireSphere(last,     10);

                // Gizmos.DrawWireCube((botLeft + topRight) / 2, topRight - botLeft);
            }
        }
    }
}