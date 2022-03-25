using System;
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

        private void OnValidate() {
            progressBar = progressBarValue;
        }
    }
}