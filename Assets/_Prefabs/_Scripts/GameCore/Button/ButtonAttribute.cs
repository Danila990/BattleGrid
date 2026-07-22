using System;
using UnityEngine;

namespace GameCore
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class ButtonAttribute : PropertyAttribute
    {
        public string ButtonName { get; }

        public ButtonAttribute(string buttonName = "Execute")
        {
            ButtonName = buttonName;
        }
    }
}
