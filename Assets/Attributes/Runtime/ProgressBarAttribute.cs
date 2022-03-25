using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UnityExtendedEditor.Attributes {
    public class ProgressBarAttribute : PropertyAttribute {
        public float Max { get; set; }

        public ProgressBarAttribute(float max = 100) {
            Max = max;
        }
    }
}