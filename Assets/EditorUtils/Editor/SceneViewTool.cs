using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.SceneManagement;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UnityExtendedEditor.EditorUtils.Editor {
    [EditorTool("Scene View Tool")]
    public class SceneViewTool : EditorTool {
        private struct ColoredAxis {
            public Vector3 direction;
            public Color color;
        }

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
            var size = HandleUtility.GetHandleSize(pos) / 3f;

            var axis = new[] {
                new ColoredAxis { direction = transform.up, color = Handles.yAxisColor },
                new ColoredAxis { direction = transform.forward, color = Handles.zAxisColor },
                new ColoredAxis { direction = transform.right, color = Handles.xAxisColor },
            };
            axis = OrderDirectionByAngleWithSceneView(axis).ToArray();
            foreach (var v in axis) {
                DrawAxisButton(pos, size, v);
            }
        }

        private void DrawAxisButton(Vector3 pos, float size, ColoredAxis axis) {
            var sceneCamForward = SceneView.currentDrawingSceneView.camera.transform.forward;
            var angle = Vector3.Angle(sceneCamForward, -axis.direction);
            if (angle < 15) return;
            
            Handles.color = axis.color;
            if (Handles.Button(pos + axis.direction * size, Quaternion.LookRotation(-axis.direction), size, size, Handles.ConeHandleCap)) {
                SetSceneViewLookAt(-axis.direction);
            }
        }

        private void SetSceneViewLookAt(Vector3 direction) {
            var sceneView = SceneView.currentDrawingSceneView;
            sceneView.rotation = Quaternion.LookRotation(direction);
        }

        private static IEnumerable<ColoredAxis> OrderDirectionByAngleWithSceneView(IEnumerable<ColoredAxis> directions) {
            var sceneDir = SceneView.currentDrawingSceneView.camera.transform.forward;
            var result = new List<ColoredAxis>(directions);
            result.Sort((d1, d2) => {
                var angle1 = Vector3.Angle(sceneDir, d1.direction);
                var angle2 = Vector3.Angle(sceneDir, d2.direction);
                return angle1.CompareTo(angle2);
            });
            return result;
        }
    }
}