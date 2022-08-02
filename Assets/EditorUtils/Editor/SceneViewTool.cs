using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.SceneManagement;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UnityExtendedEditor.EditorUtils.Editor {
    [EditorTool("Scene View Tool")]
    public class SceneViewTool : EditorTool {
        public override void OnToolGUI(EditorWindow window) {
            var view = window as SceneView;

            if (!view || !Selection.activeTransform)
                return;

            if (!StageUtility.IsGameObjectRenderedByCameraAndPartOfEditableScene(Selection.activeTransform.gameObject,
                    Camera.current))
                return;

            Vector3 handlePosition = Tools.handlePosition;
            ToolGUI(view, handlePosition);
        }

        private void ToolGUI(SceneView view, Vector3 handlePosition) {
            if (view.camera.transform.position == handlePosition)
                return;

            var transform = ((GameObject)target).transform;
            var pos = transform.position;
            var size = HandleUtility.GetHandleSize(pos) / 2f;
            
            DrawAxisButton(pos, transform.up, size, Handles.yAxisColor);
            DrawAxisButton(pos, transform.forward, size, Handles.zAxisColor);
            DrawAxisButton(pos, transform.right, size, Handles.xAxisColor);
        }

        private void DrawAxisButton(Vector3 pos, Vector3 dir, float size, Color col) {
            Handles.color = col;
            if (Handles.Button(pos + dir * size, Quaternion.LookRotation(-dir), size, size, Handles.ConeHandleCap)) {
                SetSceneViewLookAt(-dir);
            }
        }

        private void SetSceneViewLookAt(Vector3 direction) {
            var sceneView = SceneView.currentDrawingSceneView;
            sceneView.rotation = Quaternion.LookRotation(direction);
        }
    }
}