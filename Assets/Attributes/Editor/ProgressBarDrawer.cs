using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UnityExtendedEditor.Attributes.Editor {
    [CustomPropertyDrawer(typeof(ProgressBarAttribute))]
    public class ProgressBarDrawer : PropertyDrawer{
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.LabelField(position, label);
            float max = ((ProgressBarAttribute) attribute).Max;
            property.floatValue = EditorGUI.Slider(position, property.floatValue, 0, max);
            EditorGUI.ProgressBar(position, property.floatValue / max, property.floatValue.ToString());
        }
    }
}