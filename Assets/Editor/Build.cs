using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    /// <summary>
    /// Helper class for creating builds with Unity through command line
    /// </summary>
    public class Build
    {
        /// <summary>
        /// Create a new build
        /// </summary>
        /// <param name="target">Desired <see cref="BuildTarget"/> for the build</param>
        /// <param name="commandLineArguments">Command line arguments passed to Unity</param>
        public Build(BuildTarget target, string[] commandLineArguments)
        {
            //Disable development build setting
            EditorUserBuildSettings.development = false;

            //Fetch the target directory from command line arguments
            string targetDir = GetTargetPath(commandLineArguments);

            //Check that command line arguments are provided
            if (string.IsNullOrEmpty(targetDir))
                throw new ArgumentException("[JenkinsBuild] Incorrect arguments for -executeMethod. Missing output directory. Format: -executeMethod <buildclass> <output dir>");

            //Assign a path for build results
            string buildPath = $"{targetDir}/{target}";

            //For testing
            Debug.Log($"Current working directory: {Directory.GetCurrentDirectory()}");

            //Clear the target directory if it exists
            if (Directory.Exists(buildPath))
                Directory.Delete(buildPath, true);

            //Check if we are building for Android
            if (target == BuildTarget.Android)
            {
                //Check if we are creating an Android App Bundle
                if (EditorUserBuildSettings.buildAppBundle == true)
                    //Modify the build path so that we create a file to the Android folder with .aab extension
                    buildPath += $"/{PlayerSettings.productName}.aab";
                else
                    //Modify the build path so that we create a a file to the Android folder with .apk extension
                    buildPath += $"/{PlayerSettings.productName}.apk";

                //Set a keystore for signing the package
                KeystoreArgs keystoreArgs = GetKeystoreArgs(commandLineArguments);
                PlayerSettings.Android.useCustomKeystore = true;
                PlayerSettings.Android.keystoreName = keystoreArgs.keyStore;
                PlayerSettings.Android.keystorePass = keystoreArgs.storePass;
                PlayerSettings.Android.keyaliasName = keystoreArgs.keyAlias;
                PlayerSettings.Android.keyaliasPass = keystoreArgs.keyPass;
            }


            //Fetch Scenes to build
            string[] scenes = FindEditorScenes();

            //Build the app
            BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                scenes = scenes,
                target = target,
                locationPathName = buildPath
            });
        }

        /// <summary>
        /// Get the target path from command line arguments.
        /// The function searches for -executeMethod argument, skips the first argument and catches the second argument.
        /// Format example: -executeMethod class.function desired/path/for/output
        /// </summary>
        /// <param name="args">Command line arguments passed to Unity</param>
        /// <returns>String given as the command line argument</returns>
        private string GetTargetPath(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                //Check if we encountered the -executeMethod argument
                if (args[i] == "-executeMethod")
                {
                    //Make sure we have enough arguments
                    if (i + 2 < args.Length)
                        //<buildclass.method> is args[i+1], so we skip it
                        return args[i + 2];
                    else
                        throw new ArgumentException("[JenkinsBuild] Incorrect arguments for -executeMethod. Format: -executeMethod <buildclass> <output dir>");
                }
            }

            //Return empty string
            return string.Empty;
        }

        private KeystoreArgs GetKeystoreArgs(string [] args)
        {
            KeystoreArgs keystoreArgs = new KeystoreArgs();

            for (int i = 0; i < args.Length; i++)
            {
                //Check if we encountered a desired option
                if (args[i] == "--keystore")
                        keystoreArgs.keyStore = CheckKeystoreArg("--keystore", args[i + 1], keystoreArgs.keyStore);
                else if (args[i] == "--storepass")
                    keystoreArgs.storePass = CheckKeystoreArg("--storepass", args[i + 1], keystoreArgs.storePass);
                else if (args[i] == "--keyalias")
                    keystoreArgs.keyAlias = CheckKeystoreArg("--keyalias", args[i + 1], keystoreArgs.keyAlias);
                else if (args[i] == "--keypass")
                    keystoreArgs.keyPass = CheckKeystoreArg("--keypass", args[i + 1], keystoreArgs.keyPass);
            }

            return keystoreArgs;
        }

        /// <summary>
        /// Check command line option, value and variable used to determine Android keystore settings for when Unity builds an Android app
        /// </summary>
        /// <param name="option">Command line option i.e --keystore</param>
        /// <param name="value">Command line value i.e appks.keystore</param>
        /// <param name="keystoreArgsValue">Value of the variable for storing <paramref name="value"/>. Intended to be one of <see cref="KeystoreArgs"/> variables.</param>
        /// <returns></returns>
        private string CheckKeystoreArg(string option, string value, string keystoreArgsValue)
        {
            //Check that the variable in KeystoreArgs is not set yet
            if (string.IsNullOrEmpty(keystoreArgsValue))
                //Makse sure the value is not another option
                if (!value.StartsWith("--"))
                    return value;
                else
                    throw new ArgumentException($"[JenkinsBuild] Incorrect argument for {option}");
            else
                throw new ArgumentException($"[JenkinsBuild] Incorrect argument for {option}");
        }

        /// <summary>
        /// Find active Scenes in the project and return list to their respective filepaths
        /// </summary>
        /// <returns>List of filepaths to active Scenes</returns>
        private string[] FindEditorScenes()
        {
            string[] scenes = new string[EditorBuildSettings.scenes.Length];

            for (int i = 0; i < scenes.Length; i++)
                scenes[i] = EditorBuildSettings.scenes[i].path;

            return scenes;
        }
    }

}