using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityExtendedEditor.Reflection.Editor;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace UnityExtendedEditor.Attributes.Editor {
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Object), true)]
    public class CustomInspector : UnityEditor.Editor {
        private List<MethodInfo> _buttonMethods;
        
        private void OnEnable() {
            _buttonMethods = ReflectionUtils.GetAllMethodsWithAttribute<ButtonAttribute>(target);
        }

        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            // draw buttons
            if (_buttonMethods.Count > 0) {
                int methodCount = _buttonMethods.Count;
                for (int i = 0; i < methodCount; ++i) {
                    var buttonAttribute = _buttonMethods[i].GetCustomAttribute<ButtonAttribute>();
                    var buttonText = string.IsNullOrEmpty(buttonAttribute.buttonText)
                        ? _buttonMethods[i].Name
                        : buttonAttribute.buttonText;
                    if (GUILayout.Button(buttonText)) {
                        _buttonMethods[i].Invoke(target, null);
                    }
                }
            }
        }
    }
}