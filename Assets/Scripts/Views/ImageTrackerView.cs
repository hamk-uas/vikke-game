using System.Collections.Generic;
using UnityEngine;
using maxstAR;

/// <summary>
/// View used in the AR Scene to detect and track matches to Image Targets.
/// Based on example provided in the MAXST SDK
/// </summary>
public class ImageTrackerView : View
{
    private Dictionary<string, ImageTrackableBehaviour> imageTrackablesMap =
        new Dictionary<string, ImageTrackableBehaviour>();

    private CameraBackgroundBehaviour cameraBackgroundBehaviour = null;

    private GameObject _mainCamera;
    private bool cameraStartDone = false;

    public override void Awake()
    {
        base.Awake();

		//Find and fetch a reference to the Main Camera in the scene
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		//Check if we found a reference
        if(_mainCamera != null)
			//Disable the Main Camera, we are using the MAXST AR Camera instead
            _mainCamera.SetActive(false);

		AndroidRuntimePermissions.Permission[] result = AndroidRuntimePermissions.RequestPermissions("android.permission.WRITE_EXTERNAL_STORAGE", "android.permission.CAMERA");
		if (result[0] == AndroidRuntimePermissions.Permission.Granted && result[1] == AndroidRuntimePermissions.Permission.Granted)
			Debug.Log("We have all the permissions!");
		else
			Debug.Log("Some permission(s) are not granted...");

		cameraBackgroundBehaviour = FindObjectOfType<CameraBackgroundBehaviour>();
        if (cameraBackgroundBehaviour == null)
        {
            Debug.LogError("Can't find CameraBackgroundBehaviour.");
            return;
        }
    }

	void Start()
	{

		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;

		imageTrackablesMap.Clear();
		ImageTrackableBehaviour[] imageTrackables = FindObjectsOfType<ImageTrackableBehaviour>();
		foreach (var trackable in imageTrackables)
		{
			imageTrackablesMap.Add(trackable.TrackableName, trackable);
			Debug.Log("Trackable add: " + trackable.TrackableName);
		}

		TrackerManager.GetInstance().StartTracker(TrackerManager.TRACKER_TYPE_IMAGE);
		AddTrackerData();

		StartCamera();

		// For see through smart glass setting
		if (ConfigurationScriptableObject.GetInstance().WearableType == WearableCalibration.WearableType.OpticalSeeThrough)
		{
			WearableManager.GetInstance().GetDeviceController().SetStereoMode(true);

			CameraBackgroundBehaviour cameraBackground = FindObjectOfType<CameraBackgroundBehaviour>();
			cameraBackground.gameObject.SetActive(false);

			WearableManager.GetInstance().GetCalibration().CreateWearableEye(Camera.main.transform);

			// BT-300 screen is splited in half size, but R-7 screen is doubled.
			if (WearableManager.GetInstance().GetDeviceController().IsSideBySideType() == true)
			{
				// Do something here. For example resize gui to fit ratio
			}
		}
	}

	private void AddTrackerData()
	{
		foreach (var trackable in imageTrackablesMap)
		{
			if (trackable.Value.TrackerDataFileName.Length == 0)
			{
				continue;
			}

			if (trackable.Value.StorageType == StorageType.AbsolutePath)
			{
				TrackerManager.GetInstance().AddTrackerData(trackable.Value.TrackerDataFileName);
				TrackerManager.GetInstance().LoadTrackerData();
			}
			else if (trackable.Value.StorageType == StorageType.StreamingAssets)
			{
				if (Application.platform == RuntimePlatform.Android)
				{
					StartCoroutine(MaxstARUtil.ExtractAssets(trackable.Value.TrackerDataFileName, (filePah) =>
					{
						TrackerManager.GetInstance().AddTrackerData(filePah, false);
						TrackerManager.GetInstance().LoadTrackerData();
					}));
				}
				else
				{
					TrackerManager.GetInstance().AddTrackerData(Application.streamingAssetsPath + "/" + trackable.Value.TrackerDataFileName);
					TrackerManager.GetInstance().LoadTrackerData();
				}
			}
		}
	}

	private void DisableAllTrackables()
	{
		foreach (var trackable in imageTrackablesMap)
		{
			trackable.Value.OnTrackFail();
		}
	}

	public override void Update()
	{
		base.Update();

		DisableAllTrackables();

		TrackingState state = TrackerManager.GetInstance().UpdateTrackingState();

		if (state == null)
		{
			return;
		}

		cameraBackgroundBehaviour.UpdateCameraBackgroundImage(state);

		TrackingResult trackingResult = state.GetTrackingResult();

		for (int i = 0; i < trackingResult.GetCount(); i++)
		{
			Trackable trackable = trackingResult.GetTrackable(i);
			imageTrackablesMap[trackable.GetName()].OnTrackSuccess(
				trackable.GetId(), trackable.GetName(), trackable.GetPose());
		}
	}

	public void SetNormalMode()
	{
		TrackerManager.GetInstance().SetTrackingOption(TrackerManager.TrackingOption.NORMAL_TRACKING);
	}

	public void SetExtendedMode()
	{
		TrackerManager.GetInstance().SetTrackingOption(TrackerManager.TrackingOption.EXTEND_TRACKING);
	}

	public void SetMultiMode()
	{
		TrackerManager.GetInstance().SetTrackingOption(TrackerManager.TrackingOption.MULTI_TRACKING);
	}

	void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			TrackerManager.GetInstance().StopTracker();
			StopCamera();
		}
		else
		{
			StartCamera();
			TrackerManager.GetInstance().StartTracker(TrackerManager.TRACKER_TYPE_IMAGE);
		}
	}

	void OnDestroy()
	{
		imageTrackablesMap.Clear();
		TrackerManager.GetInstance().SetTrackingOption(TrackerManager.TrackingOption.NORMAL_TRACKING);
		TrackerManager.GetInstance().StopTracker();
		TrackerManager.GetInstance().DestroyTracker();
		StopCamera();

		//Check if we have a reference to the Main Camera
        if(_mainCamera != null)
			//Activate the Main Camera now that we are exiting the AR Scene
            _mainCamera.SetActive(true);
    }

    public void StartCamera()
    {
        if (!cameraStartDone)
        {
            ResultCode result = CameraDevice.GetInstance().Start();
            cameraStartDone = true;
            Debug.Log("Unity StartCamera. result : " + result);
        }
    }

    public void StopCamera()
    {
        if (cameraStartDone)
        {
            ResultCode result = CameraDevice.GetInstance().Stop();
            Debug.Log("Unity StopCamera. result : " + result);
            cameraStartDone = false;
        }
    }

	/// <summary>
	/// Function to return from the AR Scene
	/// </summary>
    public void ReturnFromARScene()
    {
        NavigationManager.Instance.ReturnFromARScene();
    }
}
