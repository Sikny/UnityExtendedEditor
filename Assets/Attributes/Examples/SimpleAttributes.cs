using UnityEngine;
using UnityExtendedEditor.Attributes;

// ReSharper disable once CheckNamespace
namespace UnityExtendedEditor.Examples {
    public class SimpleAttributes : MonoBehaviour {
        public int testInt;
        [Button(buttonText = "On Button Pressed")]
        private void OnButtonPressed() {
            Debug.Log("OnButtonPressed - Button Pressed !");
        }

        [MinMaxSlider] public Vector2 minMaxSlider;

        [ProgressBar] public float progressBar;

        [Range(0, 100)] public float progressBarValue;

        [Color(1, 0, 0)] public string stringColoredRed;
        [ColorLabel(0, 1, 0)] public string labelColoredGreen;

        private void OnValidate() {
            progressBar = progressBarValue;
        }
    }
}