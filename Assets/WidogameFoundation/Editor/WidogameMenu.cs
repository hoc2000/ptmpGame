using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using WidogameFoundation;
using WidogameFoundation.Config;

public class WidogameMenu : Editor
{
    [MenuItem("Widogame/App Settings")]
    public static void Settings() {
        if (!Directory.Exists(WidogameConstants.WIDOGAME_RESOURCES_PATH))
        {
            Directory.CreateDirectory(WidogameConstants.WIDOGAME_RESOURCES_PATH);
        }
        WidogameAppSettings appSettings = Resources.Load<WidogameAppSettings>(WidogameConstants.WIDOGAME_APP_SETTINGS_NAME);
        if (appSettings == null) {
            appSettings = CreateInstance<WidogameAppSettings>();
            AssetDatabase.CreateAsset(appSettings, WidogameAppSettings.WIDOGAME_CONFIGURATION_ASSET_PATH);
            appSettings = Resources.Load<WidogameAppSettings>(WidogameConstants.WIDOGAME_APP_SETTINGS_NAME);
        }
        Selection.activeObject = appSettings;
    }
}
