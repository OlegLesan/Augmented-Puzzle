using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;
using System.Diagnostics;
using UnityEngine;

public static class BuildPreparer
{
    [MenuItem("Build Tools/Prepare iOS Build")]
    public static void PrepareBuild()
    {
        PlayerSettings.iOS.targetOSVersionString = "12.0";
        PlayerSettings.SetArchitecture(BuildTargetGroup.iOS, 1); // arm64 only
        PlayerSettings.stripEngineCode = true;
        PlayerSettings.iOS.appleEnableAutomaticSigning = true;

        UnityEngine.Debug.Log("✅ iOS Build Settings prepared.");
    }

    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target != BuildTarget.iOS) return;

        // Подключаем Info.plist
        string plistPath = Path.Combine(pathToBuiltProject, "Info.plist");
        PlistDocument plist = new PlistDocument();
        plist.ReadFromFile(plistPath);
        PlistElementDict rootDict = plist.root;

        // Пример: разрешаем доступ к камере
        rootDict.SetString("NSCameraUsageDescription", "This app needs camera access.");

        // Сохраняем
        File.WriteAllText(plistPath, plist.WriteToString());

        // Разрешаем Bitcode (можно отключить, если проблемы)
        string projPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
        PBXProject proj = new PBXProject();
        proj.ReadFromFile(projPath);

        string targetGUID = proj.GetUnityMainTargetGuid();
        proj.SetBuildProperty(targetGUID, "ENABLE_BITCODE", "NO");

        proj.WriteToFile(projPath);

        UnityEngine.Debug.Log("📦 Xcode project patched after build.");
    }
}
