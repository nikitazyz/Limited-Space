using System.Collections;
using System.Collections.Generic;
using TimeUtility;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Cooldown))]
public class CooldownPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var timeProp = property.FindPropertyRelative("_time");
        EditorGUI.PropertyField(position, timeProp, label);
    }
}
