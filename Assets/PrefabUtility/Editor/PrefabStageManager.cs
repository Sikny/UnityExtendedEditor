using UnityEditor;
using UnityEditor.Experimental.SceneManagement;

// ReSharper disable once CheckNamespace
namespace UnityExtendedEditor.PrefabUtility.Editor
{
    [InitializeOnLoad]
    public class PrefabStageManager
    {
        static PrefabStageManager()
        {
            PrefabStage.prefabStageOpened -= OnPrefabStageOpened;
            PrefabStage.prefabStageClosing -= OnPrefabStageClosing;
            
            
            PrefabStage.prefabStageOpened += OnPrefabStageOpened;
            PrefabStage.prefabStageClosing += OnPrefabStageClosing;
        }

        private static void OnPrefabStageOpened(PrefabStage prefabStage)
        {
            if (prefabStage != null)
            {
                var root = prefabStage.prefabContentsRoot;
                if (root != null)
                {
                    var prefabStageListeners = root.GetComponentsInChildren<IPrefabStageListener>();
                    foreach (var prefabStageListener in prefabStageListeners)
                    {
                        prefabStageListener.OnPrefabOpened();
                    }
                }
            }
        }
        
        private static void OnPrefabStageClosing(PrefabStage prefabStage)
        {
            if (prefabStage != null)
            {
                var root = prefabStage.prefabContentsRoot;
                if (root != null)
                {
                    var prefabStageListeners = root.GetComponentsInChildren<IPrefabStageListener>();
                    foreach (var prefabStageListener in prefabStageListeners)
                    {
                        prefabStageListener.OnPrefabClosing();
                    }

                    UnityEditor.PrefabUtility.SaveAsPrefabAsset(root, prefabStage.assetPath);
                    UnityEditor.PrefabUtility.UnloadPrefabContents(root);
                }
            }
        }
    }
}
