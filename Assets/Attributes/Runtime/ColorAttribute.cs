using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UnityExtendedEditor.Attributes {
    public class ColorAttribute : PropertyAttribute {
        private readonly float _r;
        private readonly float _g;
        private readonly float _b;
        
        public Color TextColor => new Color(_r, _g, _b);

        public ColorAttribute(float r, float g, float b) {
            _r = r;
            _g = g;
            _b = b;
        }
    }
}