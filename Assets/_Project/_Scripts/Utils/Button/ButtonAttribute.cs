using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class ButtonAttribute : PropertyAttribute
{
    public string Name { get; private set; }
    public ButtonAttribute() : this(null) { }

    public ButtonAttribute(string name)
    {
        Name = name;
    }
}