#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace GameCore
{
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class ButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var targetObject = target as UnityEngine.Object;
            if (targetObject == null) return;

            var type = targetObject.GetType();
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                var buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();
                if (buttonAttribute != null)
                {
                    if (GUILayout.Button(buttonAttribute.ButtonName))
                    {
                        method.Invoke(targetObject, null);
                    }
                }
            }
        }
    }

    [CustomPropertyDrawer(typeof(ButtonAttribute))]
    public class ButtonDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 0;
        }
    }
}
#endif