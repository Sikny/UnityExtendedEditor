using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolbarExtender;

namespace UnityExtendedEditor.EditorUtils.Editor {
    [InitializeOnLoad]
    internal class SceneSelector {
        private static int _currentSceneIndex;

        private static string[] _sceneNames;
        
        static SceneSelector() {
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
            
            SceneManager.activeSceneChanged += OnSceneOpened;
            EditorSceneManager.activeSceneChangedInEditMode += OnSceneOpened;
        }

        private static void OnSceneOpened(Scene oldScene, Scene newScene) {
            _sceneNames = GetScenesNames();
            _currentSceneIndex = GetScenesPaths().FindIndex(p => newScene.path == p);
        }

        private static void OnToolbarGUI() {
            if (Application.isPlaying) return;
            GUIStyle style = EditorStyles.toolbarPopup;
            _sceneNames ??= GetScenesNames();
            var index = EditorGUILayout.Popup(_currentSceneIndex, _sceneNames, style, 
                GUILayout.Width(150));
            
            if(_currentSceneIndex == index)
                return;

            _currentSceneIndex = index;
            OpenScene(index);
        }

        private static void OpenScene(int index) {
            EditorSceneManager.OpenScene(GetScenesPaths()[index]);
        }

        private static string[] GetScenesNames() {
            return GetScenesPaths().Select(path => path.Split('.')[0].Split('/')
                .Last()).ToArray();
        }

        private static List<string> GetScenesPaths() {
            return AssetDatabase.FindAssets("t:scene").Select(AssetDatabase.GUIDToAssetPath).ToList();
        }
    }
}