using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyEditorStyle
{
//    public Dictionary<>
    public static void Header(string name)
    {
        var style = EditorStyles.label;
        style.fontStyle = FontStyle.Bold;
        GUILayout.Label(name, style);
    }

    public static bool Toggle(string name, bool value, int nameWidth = 100)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(name, GUILayout.Width(nameWidth));
        value = EditorGUILayout.Toggle(value);
        GUILayout.EndHorizontal();

        return value;
    }

    public static string TextField(string name, string value, int nameWidth = 100)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(name, GUILayout.Width(nameWidth));
        value = EditorGUILayout.TextField(value);
        GUILayout.EndHorizontal();

        return value;
    }

    public static int IntField(string name, int value, int nameWidth = 100)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(name, GUILayout.Width(nameWidth));
        value = EditorGUILayout.IntField(value);
        GUILayout.EndHorizontal();

        return value;
    }

    public static Enum EnumPopup(string name, Enum value, int nameWidth = 100)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(name, GUILayout.Width(nameWidth));
        value = EditorGUILayout.EnumPopup(value);
        GUILayout.EndHorizontal();

        return value;
    }

    public static void Group(string groupName, int paddingLeft, Action callBack)
    {
        GUILayout.Label(groupName);
        GUILayout.BeginHorizontal();
        GUILayout.Space(paddingLeft);
        GUILayout.BeginVertical();
        callBack();
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        
    }
}