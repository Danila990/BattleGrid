using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class StringEnumAttribute : PropertyAttribute
{
    public string Path { get; private set; }
    public Type Type { get; private set; }

    public StringEnumAttribute(string path, Type type = null) 
    {
        Path = path;
        Type = type;
    }

    public StringEnumAttribute(Type type)
    {
        Path = "";
        Type = type;
    }
}
