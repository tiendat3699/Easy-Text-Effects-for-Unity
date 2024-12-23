using TMPro;
using UnityEngine;

namespace Text_Effect
{
    [CreateAssetMenu(fileName = "Persistent_Texture", menuName = "Text Effect/Persistent/Texture")]
    public class Persistent_Texture : TextEffect_Persistent
    {
        public Texture2D texture;
        public Vector2 textureScale = Vector2.one;
        [Range(0, 1)] public float textureBlend = 1;

        public override void ApplyEffect(TMP_TextInfo _textInfo, int _charIndex)
        {
            TMP_CharacterInfo charInfo = _textInfo.characterInfo[_charIndex];
            var materialIndex = charInfo.materialReferenceIndex;
            Material material = _textInfo.meshInfo[materialIndex].material;
            
            _textInfo.meshInfo[materialIndex].

            material.SetTexture("_Tex", texture);
            material.SetFloat("_TexScaleX", textureScale.x);
            material.SetFloat("_TexScaleY", textureScale.y);
            material.SetFloat("_UseTex", textureBlend);
        }
    }
}