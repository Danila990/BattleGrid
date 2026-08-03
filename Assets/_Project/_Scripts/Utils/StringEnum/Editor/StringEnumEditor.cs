#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StringEnumAttribute))]
public class StringEnumEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var attrib = this.attribute as StringEnumAttribute;
        List<string> nameList = new List<string>() { "<Not Select>" };
        foreach (var item in Resources.LoadAll(attrib.Path))
            if(item.GetType().Equals(attrib.Type))
                nameList.Add(item.name);

        string propertyString = property.stringValue;
        int index = -1;
        if (propertyString == "")
            index = 0;
        else
            for (int i = 1; i < nameList.Count; i++)
            {
                if (attrib.Path + "/" + nameList[i] == propertyString)
                {
                    index = i;
                    break;
                }
            }

        index = EditorGUI.Popup(position, label.text, index, nameList.ToArray());

        if (index >= 1)
            property.stringValue = attrib.Path +"/" + nameList[index];
        else
            property.stringValue = "";

        EditorGUI.EndProperty();
    }
}
#endif