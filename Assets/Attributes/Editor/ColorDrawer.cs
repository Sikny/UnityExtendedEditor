﻿using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UnityExtendedEditor.Attributes.Editor {
    [CustomPropertyDrawer(typeof(ColorAttribute))]
    public class ColorDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var guiColor = GUI.contentColor;
            EditorGUI.LabelField(position, label);
            GUI.contentColor = ((ColorAttribute)attribute).TextColor;
            EditorGUI.PropertyField(position, property);
            GUI.contentColor = guiColor;
        }
    }
}