﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AutoIncrementVersionCodeInCloudBuild : MonoBehaviour
{
#if UNITY_CLOUD_BUILD

    public static void PreExport(UnityEngine.CloudBuild.BuildManifestObject manifest)
    {
        string buildNumber = manifest.GetValue("buildNumber", "0");
        Debug.LogWarning("Setting build number to " + buildNumber);
        PlayerSettings.Android.bundleVersionCode = int.Parse(buildNumber);
        PlayerSettings.iOS.buildNumber = buildNumber;
        EditorUserBuildSettings.androidCreateSymbolsZip = true;
    }
#endif
}
