using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using System.Collections;
using System.IO;

public class EntitlementsPostProcess : ScriptableObject
{
    //private const string ENTITLEMENT_CONTENT = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<!DOCTYPE plist PUBLIC \"-//Apple//DTD PLIST 1.0//EN\" \"http://www.apple.com/DTDs/PropertyList-1.0.dtd\">\r\n<plist version=\"1.0\">\r\n<dict>\r\n\t<key>aps-environment</key>\r\n\t<string>production</string>\r\n</dict>\r\n</plist>";

#if UNITY_IOS
    [PostProcessBuild]
    public static void OnPostProcess(BuildTarget buildTarget, string buildPath) {
        if (buildTarget != BuildTarget.iOS) {
            return;
        }

        var proj_path = PBXProject.GetPBXProjectPath(buildPath);
        var proj = new PBXProject();
        proj.ReadFromFile(proj_path);

        // target_name = "Unity-iPhone"
        var target_guid = proj.GetUnityMainTargetGuid();
        var frameworkGuid = proj.GetUnityFrameworkTargetGuid();
        //var target_name = "Unity-iPhone";
        string filename = "Production.entitlements";
        //var dst = buildPath + "/" + target_name + "/" + filename;
        //StreamWriter writer = new StreamWriter(dst);
        //writer.Write(ENTITLEMENT_CONTENT);
        //writer.Close();
        //proj.AddFile(target_name + "/" + filename, filename);
        //proj.AddBuildProperty(target_guid, "CODE_SIGN_ENTITLEMENTS", target_name + "/" + filename);
        //proj.AddFrameworkToProject(target_guid, "UserNotifications.framework", false);
        //proj.AddFrameworkToProject(target_guid, "AdSupport.framework", false);
        //proj.AddFrameworkToProject(target_guid, "DeviceCheck.framework", false);
        //proj.AddFrameworkToProject(frameworkGuid, "DeviceCheck.framework", false);
        //proj.AddFrameworkToProject(frameworkGuid, "AdSupport.framework", false);

        proj.WriteToFile(proj_path);

        var capacilities = new ProjectCapabilityManager(proj_path, filename, null, target_guid);
        //capacilities.AddAssociatedDomains(new string[] { "applinks:angryballadventure.page.link" });
        capacilities.AddBackgroundModes(BackgroundModesOptions.RemoteNotifications);
        capacilities.AddPushNotifications(Debug.isDebugBuild);
        //capacilities.AddSignInWithAppleWithCompatibility(frameworkGuid);
        capacilities.WriteToFile();

    }
#endif
}
