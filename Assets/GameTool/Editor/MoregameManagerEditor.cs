//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;


//[CustomEditor(typeof(MoregameManager))]
//public class MoregameManagerEditor : Editor
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

//        GUILayout.BeginHorizontal();
//        if (GUILayout.Button("Update Moregame Android"))
//        {
//            MoregameManager api = target as MoregameManager;
//            api.LoadMoregameFromGoogleSheet(api.AndroidMoregameInfors, true);
//        }

//        if (GUILayout.Button("Update Moregame IOS"))
//        {
//            MoregameManager api = target as MoregameManager;
//            api.LoadMoregameFromGoogleSheet(api.IosMoregameInfors, false);
//        }

//        GUILayout.EndHorizontal();
//    }

//    public void CopyKeyAndroid()
//    {
//        MoregameManager api = target as MoregameManager;
//        string data = api.GetKey();
//        EditorGUIUtility.systemCopyBuffer = data;
//    }

//    public void CopyKeyIOS()
//    {
//        MoregameManager api = target as MoregameManager;
//        string data = api.GetKey();
//        EditorGUIUtility.systemCopyBuffer = data;
//    }

//    public void CopyDataAndroid()
//    {
//        MoregameManager api = target as MoregameManager;
//        string data = JsonUtility.ToJson(api.AndroidMoregameInfors, true);
//        EditorGUIUtility.systemCopyBuffer = data;
//    }

//    public void CopyDataIOS()
//    {
//        MoregameManager api = target as MoregameManager;
//        string data = JsonUtility.ToJson(api.IosMoregameInfors, true);
//        EditorGUIUtility.systemCopyBuffer = data;
//    }
//}