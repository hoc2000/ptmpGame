using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class MyBuildWindow : EditorWindow
{
    private static string projectFoler;
    private static string path;

    // Ham mo window
    [MenuItem("Window/BuildTool %r")]
    public static void OpenLevelEditorWindow()
    {
        MyBuildWindow window = (MyBuildWindow)GetWindow(typeof(MyBuildWindow));
        window.Show();
    }

    void OnGUI()
    {
        PlayerPrefs.SetString("BuildVersion", EditorGUILayout.TextField("Version", PlayerPrefs.GetString("BuildVersion")));
        PlayerPrefs.SetInt("BuildArmVerCode", EditorGUILayout.IntField("Version code (ARM)", PlayerPrefs.GetInt("BuildArmVerCode")));
        PlayerPrefs.SetInt("BuildX86VerCode", EditorGUILayout.IntField("Version code (X86)", PlayerPrefs.GetInt("BuildX86VerCode")));

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Build Arm"))
        {
            BuildArm();
        }

        if (GUILayout.Button("Build X86"))
        {
            BuildX86();
        }

        if (GUILayout.Button("Build Arm & X86"))
        {
            BuildArmAndX86();
        }

        if (GUILayout.Button("Build All"))
        {
            BuildAll();
        }

        EditorGUILayout.EndHorizontal();
        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        PlayerPrefs.SetString("BuildHashKey", EditorGUILayout.TextField("Hash Key", PlayerPrefs.GetString("BuildHashKey")));
        if (GUILayout.Button("Get Hash Key", GUILayout.ExpandWidth(false)))
        {
            GetHashKey();
        }
        EditorGUILayout.EndHorizontal();
    }



    private void BuildAll()
    {
        BuildX86();
        BuildArm();
    }

    private void BuildArmAndX86()
    {
        CheckKeyStore();

        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
        PlayerSettings.Android.keyaliasName = PlayerSettings.applicationIdentifier;
        PlayerSettings.bundleVersion = PlayerPrefs.GetString("BuildVersion");

        path = Path.Combine(projectFoler, "AndroidBuilds");
        Debug.Log(path);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        PlayerSettings.Android.bundleVersionCode = PlayerPrefs.GetInt("BuildArmVerCode");
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.All;
        BuildPipeline.BuildPlayer(GetScenePaths(), Path.Combine(path, string.Format("{0}_{1}_{2}_{3}_{4}",
            PlayerSettings.productName.ToLower().Replace(" ", ""),
            PlayerSettings.applicationIdentifier.ToLower(),
            PlayerSettings.bundleVersion,
            PlayerSettings.Android.bundleVersionCode,
            "fat.apk"
        )), BuildTarget.Android, BuildOptions.None);
    }

    private void BuildX86()
    {
        CheckKeyStore();

        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
        PlayerSettings.Android.keyaliasName = PlayerSettings.applicationIdentifier;
        PlayerSettings.bundleVersion = PlayerPrefs.GetString("BuildVersion");

        path = Path.Combine(projectFoler, "AndroidBuilds");
        Debug.Log(path);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        PlayerSettings.Android.bundleVersionCode = PlayerPrefs.GetInt("BuildX86VerCode");
        //PlayerSettings.Android.targetArchitectures = AndroidArchitecture.X86;
        BuildPipeline.BuildPlayer(GetScenePaths(), Path.Combine(path, string.Format("{0}_{1}_{2}_{3}_{4}",
            PlayerSettings.productName.ToLower().Replace(" ", ""),
            PlayerSettings.applicationIdentifier.ToLower(),
            PlayerSettings.bundleVersion,
            PlayerSettings.Android.bundleVersionCode,
            "x86.apk"
        )), BuildTarget.Android, BuildOptions.None);
    }

    private void BuildArm()
    {
        CheckKeyStore();

        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
        PlayerSettings.Android.keyaliasName = PlayerSettings.applicationIdentifier;
        PlayerSettings.bundleVersion = PlayerPrefs.GetString("BuildVersion");

        path = Path.Combine(projectFoler, "AndroidBuilds");
        Debug.Log(path);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        PlayerSettings.Android.bundleVersionCode = PlayerPrefs.GetInt("BuildArmVerCode");
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7;
        BuildPipeline.BuildPlayer(GetScenePaths(), Path.Combine(path, string.Format("{0}_{1}_{2}_{3}_{4}",
            PlayerSettings.productName.ToLower().Replace(" ", ""),
            PlayerSettings.applicationIdentifier.ToLower(),
            PlayerSettings.bundleVersion,
            PlayerSettings.Android.bundleVersionCode,
            "arm.apk"
        )), BuildTarget.Android, BuildOptions.CompressWithLz4HC);
    }

    public static void CheckKeyStore()
    {
        projectFoler = Path.GetDirectoryName(Application.dataPath);
        //string keyStorePath = Path.Combine(projectFoler, PlayerSettings.bundleIdentifier + ".keystore");
        string keyStorePath = projectFoler + "/" + PlayerSettings.applicationIdentifier + ".keystore";
        string pass = "123456789";
        // Check tồn tại file keytore
        if (File.Exists(keyStorePath))
        {
            PlayerSettings.Android.keystoreName = keyStorePath;
        }
        else
        {
            // Tao new keystore 
            RunCmd(string.Format("keytool -genkey -v -keystore \"{0}\" -alias {1} -keyalg RSA -keysize 2048 -validity 10000 -dname \"CN =, OU =, O =, L =, S =, C = \" -storepass {2} -keypass {3}",
                keyStorePath,
                PlayerSettings.applicationIdentifier,
                pass,
                pass
            ));
            Debug.Log("Đã tạo keystore mới: " + keyStorePath);
            PlayerSettings.Android.keystoreName = keyStorePath;
        }
        PlayerSettings.keystorePass = pass;
        PlayerSettings.keyaliasPass = pass;
    }

    private void GetHashKey()
    {
        projectFoler = Path.GetDirectoryName(Application.dataPath);
        string keyStorePath = projectFoler + "/" + PlayerSettings.applicationIdentifier + ".keystore";
        string command = string.Format
            ("keytool -exportcert -alias {0} -keystore \"{1}\" | openssl sha1 -binary |\"C:\\OpenSSL\\bin\\openssl\" base64",
            PlayerSettings.applicationIdentifier,
            keyStorePath);

        var output = RunCmdWithOutput(command);
        PlayerPrefs.SetString("BuildHashKey", output);
        Debug.Log("cmd output: " + output);
    }

    public static void RunCmd(string cmdString)
    {
        Debug.Log(cmdString);
        String command = "/C " + cmdString;
        ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe");
        cmdsi.Arguments = command;
        Process cmd = Process.Start(cmdsi);
        cmd.WaitForExit();
    }

    public static string RunCmdWithOutput(string cmdString)
    {
        Debug.Log(cmdString);
        String command = "/C " + cmdString;
        ProcessStartInfo cmdsi = new ProcessStartInfo("cmd.exe");
        cmdsi.UseShellExecute = false;
        cmdsi.RedirectStandardOutput = true;
        cmdsi.RedirectStandardInput = true;
        cmdsi.Arguments = command;

        Process cmd = Process.Start(cmdsi);
        cmd.StandardInput.WriteLine("123456789");
        return cmd.StandardOutput.ReadToEnd();
    }

    static string[] GetScenePaths()
    {
        string[] scenes = new string[EditorBuildSettings.scenes.Length];

        for (int i = 0; i < scenes.Length; i++)
            scenes[i] = EditorBuildSettings.scenes[i].path;

        return scenes;
    }
}
