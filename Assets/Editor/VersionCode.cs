using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    /// <summary>
    /// Class for handling version code and build number for Android/iOS cross platform projects
    /// Original by Stencil Ltd
    /// https://github.com/stencil-ltd/Stencil-Build/blob/master/Editor/BuildScript.cs
    /// </summary>
    public class VersionCode
    {
        /// <summary>
        /// Increase Android version code and iOS build number by one
        /// </summary>
        [MenuItem("Version Codes/Bump Versions")]
        public static void BumpVersions()
        {
            //Make sure both platforms have the same version
            SyncVersionCodes();
            //Bump versions
            PlayerSettings.Android.bundleVersionCode++;
            PlayerSettings.iOS.buildNumber = "" + (int.Parse(PlayerSettings.iOS.buildNumber) + 1);
            //Write new versions to JSON file
            WriteVersionCodes();
        }

        /// <summary>
        /// Synchronize Android version code and iOS build number
        /// </summary>
        [MenuItem("Version Codes/Sync Versions")]
        public static void SyncVersionCodes()
        {
            //Fetch iOS build number
            var ios = int.Parse(PlayerSettings.iOS.buildNumber);
            //Fetch Android bundle version code
            var android = PlayerSettings.Android.bundleVersionCode;
            //Compare which has larger value
            var max = Math.Max(ios, android);
            //Set Android bundle version code
            PlayerSettings.Android.bundleVersionCode = max;
            //Set iOS build number
            PlayerSettings.iOS.buildNumber = $"{max}";
            //Check if the version codes need to be rewritten
            //if(ios != android || ios != max || android != max)
                //Write new versions to JSON file
                WriteVersionCodes();
        }

        /// <summary>
        /// Write Android version code and name, and iOS build number and version to a JSON file
        /// </summary>
        [MenuItem("Version Codes/Write Version")]
        public static void WriteVersionCodes()
        {
            Debug.Log("Writing Version Codes");
            if (!Directory.Exists("Assets/Resources"))
                Directory.CreateDirectory("Assets/Resources");

            var path = "Assets/Resources/VersionCodes.json";
            var writer = new StreamWriter(path, false);
            var json = new VersionJson
            {
                android = new VersionPlatform
                {
                    versionCode = PlayerSettings.Android.bundleVersionCode,
                    versionString = PlayerSettings.bundleVersion
                },
                ios = new VersionPlatform
                {
                    versionCode = int.Parse(PlayerSettings.iOS.buildNumber),
                    versionString = PlayerSettings.bundleVersion
                }
            };

            writer.Write(JsonUtility.ToJson(json));
            writer.Close();
            AssetDatabase.ImportAsset(path);
            EditorApplication.ExecuteMenuItem("File/Save");
        }

        /// <summary>
        /// Print out the current version and build number
        /// </summary>
        [MenuItem("Version Codes/Print Version")]
        public static void PrintVersionCodes()
        {
            //First make sure versions are in sync
            SyncVersionCodes();
            //Print out version and build number
            Debug.Log($"Current version: {PlayerSettings.bundleVersion}");
            Debug.Log($"Android bundle code: {PlayerSettings.Android.bundleVersionCode}");
            Debug.Log($"iOS build number: {PlayerSettings.iOS.buildNumber}");
        }
    }
}