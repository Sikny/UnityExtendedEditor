// ReSharper disable once CheckNamespace
namespace UnityExtendedEditor.PrefabUtility
{
    public interface IPrefabStageListener
    {
        void OnPrefabOpened();
        void OnPrefabClosing();
    }
}
