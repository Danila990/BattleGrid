#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(UnityEngine.Object), true)]
public class ButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (!(target is MonoBehaviour) && !(target is ScriptableObject))
            return;

        var methods = target.GetType().GetMethods(
            BindingFlags.Instance |
            BindingFlags.Static |
            BindingFlags.Public |
            BindingFlags.NonPublic);

        bool hasButtons = false;

        foreach (var method in methods)
        {
            var buttonAttr = (ButtonAttribute)Attribute.GetCustomAttribute(
                method, typeof(ButtonAttribute));

            if (buttonAttr != null && method.GetParameters().Length == 0)
            {
                hasButtons = true;
                break;
            }
        }

        if (!hasButtons) return;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Buttons", EditorStyles.boldLabel);

        foreach (var method in methods)
        {
            var buttonAttr = (ButtonAttribute)Attribute.GetCustomAttribute(
                method, typeof(ButtonAttribute));

            if (buttonAttr != null && method.GetParameters().Length == 0)
            {
                string buttonName = string.IsNullOrEmpty(buttonAttr.Name)
                    ? ObjectNames.NicifyVariableName(method.Name)
                    : buttonAttr.Name;

                if (GUILayout.Button(buttonName))
                {
                    foreach (var t in targets)
                    {
                        method.Invoke(t, null);
                    }
                }
            }
        }
    }
}
#endif