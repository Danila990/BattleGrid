using UnityEditor;
using UnityEngine;

namespace BattleGridGame.GridEditor
{
    public static class GridEditorExtension
    {
        public static void CustomPropetry(Object @object, string propertyName)
        {
            if (@object == null) return;

            SerializedObject serializedObject = new SerializedObject(@object);
            SerializedProperty containerProperty = serializedObject.FindProperty(propertyName);
            EditorGUILayout.PropertyField(containerProperty, true);
            serializedObject.ApplyModifiedProperties();
        }

        public static void CustomPropetry(Object @object)
        {
            if (@object == null) return;

            SerializedObject serializedObject = new SerializedObject(@object);
            serializedObject.ApplyModifiedProperties();
        }

        public static void MidlleText(string text, int fontSize, int space = 0)
        {
            EditorGUILayout.Space(space);
            GUIStyle centeredStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = fontSize
            };
            EditorGUILayout.LabelField(text, centeredStyle, GUILayout.ExpandWidth(true));
        }
    }
}