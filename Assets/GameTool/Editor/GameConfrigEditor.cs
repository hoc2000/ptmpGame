//using System.IO;
//using System.Xml;
//using UnityEditor;
//using UnityEngine;


//[CustomEditor(typeof(GameConfig))]
//public class GameConfrigEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        GUILayout.BeginHorizontal();
//        if (GUILayout.Button("Copy key"))
//            CopyKey();
//        if (GUILayout.Button("Copy Data"))
//            CopyData();
//        GUILayout.EndHorizontal();
//    }

//    public void CopyKey()
//    {
//        GameConfig gameConfig = target as GameConfig;
//        string data = GameConfig.Instance.GetKey();
//        EditorGUIUtility.systemCopyBuffer = data;
//    }
//    public void CopyData()
//    {
//        GameConfig gameConfig = target as GameConfig;
//        string data = JsonUtility.ToJson(gameConfig.valueConfrig, true);
//        EditorGUIUtility.systemCopyBuffer = data;
//    }
//}