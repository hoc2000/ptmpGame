using System.Collections;
using System.Collections.Generic;
using UnityEditor;
#if UNITY_IOS
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class SDKXCodeProjectProcessor
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string path) {
        string projPath = PBXProject.GetPBXProjectPath(path);

        var project = new PBXProject();
        project.ReadFromFile(projPath);

        string unityFrameworkGuid = project.GetUnityFrameworkTargetGuid();
        project.SetBuildProperty(unityFrameworkGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");
        project.WriteToFile(projPath);
    }

}
#endif