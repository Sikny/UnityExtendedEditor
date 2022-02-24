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
    }
}