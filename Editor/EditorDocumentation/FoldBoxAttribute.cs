#if UNITY_EDITOR
using UnityEngine;

namespace EasyTextEffects.Editor.EditorDocumentation
{
    public class FoldBoxAttribute : PropertyAttribute
    {
        public enum ContentType
        {
            Text, Image
        }
        public string Title { get; }
        public string[] Content { get; }
        public ContentType[] ContentTypes { get; }

        public FoldBoxAttribute(string _title, string[] _content, ContentType[] _contentTypes)
        {
            Title = _title;
            Content = _content;
            ContentTypes = _contentTypes;
        }
    }
}
#endif