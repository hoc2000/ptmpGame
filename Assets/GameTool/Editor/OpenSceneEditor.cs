using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSceneEditor : EditorWindow
{
    Vector2 ScrollPos = Vector2.zero;

    [MenuItem("Open Scene/ Open All %r")]
    public static void OpenLevelEditorWindow()
    {
        OpenSceneEditor window = (OpenSceneEditor)GetWindow(typeof(OpenSceneEditor));
        window.Show();
    }

    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    void OnGUI()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos,GUI.skin.box);
        foreach (EditorBuildSettingsScene item in EditorBuildSettings.scenes)
        {
            if (GUILayout.Button("Open " + GetSceneNameFromPath(item.path), GUILayout.Width(500)))
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(item.path);
                }
            }
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    string GetSceneNameFromPath(string path)
    {
          return path;//SceneManager.GetSceneByPath(path).name;
    }

    [MenuItem("Open Scene/SPL")]
    public static void OpenSceneSPL()
    {
        string localPath = "Assets/CraftmanHero/Scenes/SPL.unity";
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene(localPath);
    }

    [MenuItem("Open Scene/HomeScene")]
    public static void OpenSceneGameHome()
    {
        string localPath = "Assets/CraftmanHero/Scenes/HomeScene.unity";
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene(localPath);
    }
    [MenuItem("Open Scene/Level")]
    public static void OpenSceneGamePlay()
    {
        string localPath = "Assets/CraftmanHero/Scenes/GamePlay.unity";
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene(localPath);
    }
}

