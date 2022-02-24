using System;

// ReSharper disable once CheckNamespace
namespace UnityExtendedEditor.Attributes {
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : Attribute {
        public string buttonText;
    }
}