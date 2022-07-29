// ReSharper disable once CheckNamespace
namespace UnityExtendedEditor.PrefabUtility {
    /// <summary>
    /// Interface for custom prefab open/close behaviour
    /// </summary>
    public interface IPrefabStageListener {
        void OnPrefabOpened();
        void OnPrefabClosing();
    }
}
