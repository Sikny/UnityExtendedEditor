using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UnityExtendedEditor.FunctionPlotter{
    public class FunctionPlotterWindow : EditorWindow
    {
        [MenuItem("Window/Function Plotter")]
        static void Init()
        {
            FunctionPlotterWindow window = (FunctionPlotterWindow) GetWindow(typeof(FunctionPlotterWindow));
            window.titleContent.text = "Function Plotter";
            window.Show();
        }
    }
}
