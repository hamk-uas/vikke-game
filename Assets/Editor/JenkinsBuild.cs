using UnityEditor;
using System;
using Assets.Editor;

/// <summary>
/// Class for build functions used by Jenkins.
/// </summary>
public class JenkinsBuild
{
    /// <summary>
    /// Build Android App Bundle (AAB) archive with Gradle
    /// Takes in build result filepath as command line parameters.
    /// Example: -executeMethod JenkinsBuild.BuildAndroid ~/Desktop/Builds
    /// </summary>
    public static void BuildAndroid()
    {
        try
        {
            //Fetch command line arguments
            string[] args = Environment.GetCommandLineArgs();
            //Indicate that we want to create an AAB instead of APK
            EditorUserBuildSettings.buildAppBundle = true;
            //Set correct build system for an AAB
            EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
            //Commence new build
            new Build(BuildTarget.Android, args);
        }
        //Catch command line argument exception
        catch (ArgumentException ex)
        {
            //Output error for Jenkins console
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// Create iOS build.
    /// Takes in build result filepath as command line parameters.
    /// Example: -executeMethod JenkinsBuild.BuildAndroid ~/Desktop/Builds
    /// </summary>
    public static void BuildiOS()
    {
        try
        {
            //Fetch command line arguments
            string[] args = Environment.GetCommandLineArgs();
            //Commence new build
            new Build(BuildTarget.iOS, args);
        }
        //Catch command line argument exception
        catch (ArgumentException ex)
        {
            //Output error for Jenkins console
            Console.WriteLine(ex.Message);
        }
    }
}