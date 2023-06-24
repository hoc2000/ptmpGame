//using System.IO;
//using System.Xml;
//using UnityEditor;
//using UnityEngine;
//[CustomEditor(typeof(SDK))]
//public class AppEditor : Editor
//{

//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        GUILayout.BeginHorizontal();
//        if (GUILayout.Button("Copy key Android"))
//            CopyKeyAndroid();
//        if (GUILayout.Button("Copy key iOS"))
//            CopyKeyIOS();
//        GUILayout.EndHorizontal();


//        GUILayout.BeginHorizontal();
//        if (GUILayout.Button("Copy Android"))
//            CopyDataAndroid();
//        if (GUILayout.Button("Copy iOS"))
//            CopyDataIOS();
//        GUILayout.EndHorizontal();


//    }

//    public void CopyKeyAndroid()
//    {
//        SDK app = target as SDK;
//        string data = app.GetKey();
//        EditorGUIUtility.systemCopyBuffer = data;
//    }

//    public void CopyKeyIOS()
//    {
//        SDK app = target as SDK;
//        string data = app.GetKey();
//        EditorGUIUtility.systemCopyBuffer = data;
//    }

//    public void CopyDataAndroid()
//    {
//        SDK app = target as SDK;
//        string data = JsonUtility.ToJson(app.AndroidAppInfor, true);
//        EditorGUIUtility.systemCopyBuffer = data;
//    }

//    public void CopyDataIOS()
//    {
//        SDK app = target as SDK;
//        string data = JsonUtility.ToJson(app.AndroidAppInfor, true);
//        EditorGUIUtility.systemCopyBuffer = data;
//    }
//}
