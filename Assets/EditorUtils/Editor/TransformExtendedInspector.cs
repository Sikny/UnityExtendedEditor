using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace EditorUtils.Editor {
    [CustomEditor(typeof(Transform))]
    [CanEditMultipleObjects]
    public class TransformExtendedInspector : UnityEditor.Editor{
        UnityEditor.Editor _defaultEditor;  // unity built-in editor
        private Transform _targetTransform;
        private static bool _worldSpaceFoldout;

        private void OnEnable() {
            _defaultEditor = CreateEditor(targets, Type.GetType("UnityEditor.TransformInspector, UnityEditor"));
            _targetTransform = target as Transform;
        }

        private void OnDisable() {
            MethodInfo disableMethod = _defaultEditor.GetType().GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (disableMethod != null)
                disableMethod.Invoke(_defaultEditor,null);
            DestroyImmediate(_defaultEditor);
        }

        public override void OnInspectorGUI() {
            _defaultEditor.OnInspectorGUI();
        
            EditorGUILayout.Space();

            //Show World Space Transform
            _worldSpaceFoldout = EditorGUILayout.Foldout(_worldSpaceFoldout, "World Space", true);
            
            if (_worldSpaceFoldout) {
                EditorGUI.BeginChangeCheck();
                var targetPos = EditorGUILayout.Vector3Field("Position", _targetTransform.position);
                var targetRot = EditorGUILayout.Vector3Field("Rotation", _targetTransform.eulerAngles);

                if (EditorGUI.EndChangeCheck()) {
                    Undo.RecordObject(target, "Move transform");
                    _targetTransform.position = targetPos;
                    Undo.RecordObject(target, "Rotate transform");
                    _targetTransform.eulerAngles = targetRot;
                    
                    EditorUtility.SetDirty(_targetTransform);
                }

                GUI.enabled = false;
                EditorGUILayout.Vector3Field("Scale", _targetTransform.lossyScale);
                GUI.enabled = true;
            }
        }
    }
}