/*==============================================================================
Copyright 2017 Maxst, Inc. All Rights Reserved.
==============================================================================*/

using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace maxstAR
{
	[CustomEditor(typeof(QrCodeTrackableBehaviour))]
	public class QrCodeTrackableEditor : Editor
	{
        private QrCodeTrackableBehaviour qrCodeTrackableBehaviour;

		public void OnEnable()
		{
			if (PrefabUtility.GetPrefabType(target) == PrefabType.Prefab)
			{
				return;
			}
		}

		public override void OnInspectorGUI()
		{
            bool isDirty = false;

            qrCodeTrackableBehaviour = (QrCodeTrackableBehaviour)target;

            EditorGUILayout.Separator();

            string oldSearchingWords = qrCodeTrackableBehaviour.QrCodeSearchingWords;
            string newSearchingWords = EditorGUILayout.TextField("QR-Code Searching words : ", qrCodeTrackableBehaviour.QrCodeSearchingWords);

            if (oldSearchingWords != newSearchingWords)
            {
                qrCodeTrackableBehaviour.QrCodeSearchingWords = newSearchingWords;
                isDirty = true;
            }

            EditorGUILayout.Separator();
            EditorGUILayout.HelpBox("The default value is -1. you must input the actual value(m) when using QRCode Fusion Tracker", MessageType.Info);
            EditorGUILayout.Separator();

            float oldQRSize = qrCodeTrackableBehaviour.QrCodeRealSize;
            float newQRSize = EditorGUILayout.FloatField("QRCode Real Size : ", qrCodeTrackableBehaviour.QrCodeRealSize);

            if (oldQRSize != newQRSize)
            {
                qrCodeTrackableBehaviour.QrCodeRealSize = newQRSize;
                isDirty = true;
            }

            if (GUI.changed && isDirty)
			{
                EditorUtility.SetDirty(qrCodeTrackableBehaviour);
				EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
				SceneManager.Instance.SceneUpdated();
			}
		}
	}
}