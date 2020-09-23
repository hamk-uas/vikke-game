using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager resposible for handling navigating between views within the application. It is also responsible for switching between the main scene and the AR scene.
/// </summary>
public class NavigationManager : Singleton<NavigationManager>
{
    /// <summary>
    /// First <see cref="View"/> of the application shown to user
    /// </summary>
    [SerializeField] private GameObject _initView;

    /// <summary>
    /// <see cref="View"/> that gets shown after user returns from the AR Scene
    /// </summary>
    [SerializeField] private GameObject _returnFromARSceneView;

    /// <summary>
    /// <see cref="Canvas"/> used as the parent for the spawned <see cref="View"/> prefabs
    /// </summary>
    [SerializeField] private Canvas _canvas;

    /// <summary>
    /// List of possible <see cref="NavigationPath"/>s used by the <see cref="NavigationManager"/>
    /// </summary>
    [SerializeField] private List<NavigationPath> _uiPaths;

    /// <summary>
    /// Current active <see cref="View"/>
    /// </summary>
    private GameObject _activeView;

    /// <summary>
    /// Used to determine if loading a <see cref="Scene"/> has been completed
    /// </summary>
    private bool sceneLoadDone;

    /// <summary>
    /// Return current active <see cref="View"/>
    /// </summary>
    /// <returns>Current active <see cref="View"/></returns>
    public GameObject GetActiveView()
    {
        return _activeView;
    }

#if UNITY_EDITOR
    /// <summary>
    /// Construct a new <see cref="NavigationManager"/>, used for testing
    /// </summary>
    /// <param name="initView">First <see cref="View"/> of the application shown</param>
    /// <param name="canvas"><see cref="Canvas"/> used as the parent for the spawned <see cref="View"/> prefabs</param>
    /// <param name="uiPaths">List of valid <see cref="NavigationPath"/>s used by the <see cref="NavigationManager"/></param>
    public void Constructor(GameObject initView, Canvas canvas, List<NavigationPath> uiPaths)
    {
        _initView = initView;
        _canvas = canvas;
        _uiPaths = uiPaths;
    }
#endif

    /// <summary>
    /// Called by Unity
    /// </summary>
    private void Start()
    {
        //Prevent destroying the manager when transitioning between Main- and AR Scenes
        DontDestroyOnLoad(this);

        //Clear the canvas
        foreach (Transform child in _canvas.transform)
            Destroy(child.gameObject);

        //Instantiate the initial view
        _activeView = Instantiate(_initView, _canvas.transform);
    }

    /// <summary>
    /// Replace the currently active and shown <see cref="View"/>
    /// </summary>
    /// <param name="newView"><see cref="View"/> that replaces the currently active one</param>
    private void ReplaceActiveView(GameObject newView)
    {
        //Check if we have a reference to the currently active view
        if(_activeView != null)
            Destroy(_activeView);

        _activeView = Instantiate(newView, _canvas.transform);
    }

    /// <summary>
    /// Respond to a call by the <paramref name="caller"/> <see cref="View"/> to navigate to the intended <paramref name="direction"/> in the application
    /// </summary>
    /// <param name="caller"><see cref="View"/> making the call</param>
    /// <param name="direction">The intended <see cref="NavigationDirection"/></param>
    public void Navigate(GameObject caller, NavigationDirection direction)
    {
        //Iterate through the valid UI paths
        foreach (NavigationPath uiPath in _uiPaths)
            //Check if there is a valid UI Path for the caller View
            //Because the prefabs are usually instantiated as clones, we need to check for name + (Clone) too
            if (uiPath.CallerView.name + "(Clone)" == caller.name || uiPath.CallerView.name == caller.name)
                //Check if the direction in the UI path and the parameter direction match
                if (uiPath.Direction == direction)
                    //Navigate to the destination View
                    ReplaceActiveView(uiPath.DestinationView);
    }

    /// <summary>
    /// Navigation function used by <see cref="StorytellingPresenter"/>
    /// This is a workaround to bypass the limitations in the implementation of the NavigationManager
    /// </summary>
    /// <param name="destination">The destination <see cref="View"/></param>
    public void Navigate(GameObject destination)
    {
        ReplaceActiveView(destination);
    }

    /// <summary>
    /// Called when returning from the AR Scene
    /// </summary>
    public void ReturnFromARScene()
    {
        StartCoroutine(OpenMainScene());
    }

    /// <summary>
    /// Coroutine to load and open MainScene
    /// </summary>
    private IEnumerator OpenMainScene()
    {
        //Start a coroutine to load and open the MainScene
        StartCoroutine(LoadScene("MainScene"));

        //We loop here until the LoadScene coroutine has finished
        //If the coroutine isn't finished, we wait for 100ms and then check again
        while (!sceneLoadDone)
            yield return new WaitForSeconds(0.1f);

        //The reference to the Canvas in the scene is lost when moving from Main to AR Scene,
        //so we need to find a new reference to after the LoadScene coroutine has completed
        if (!_canvas)
            _canvas = FindObjectOfType<Canvas>();

        //Instantiate the view intended to be shown after returning from AR Scene
        ReplaceActiveView(_returnFromARSceneView);
    }

    /// <summary>
    /// Function to load and open AR Scene
    /// </summary>
    public void OpenARScene()
    {
        StartCoroutine(LoadScene("ARScene"));
    }

    /// <summary>
    /// Function to load an open a Scene
    /// </summary>
    /// <param name="sceneName">Name of the Scene</param>
    private IEnumerator LoadScene(string sceneName)
    {
        //Reset the completion indicator
        sceneLoadDone = false;

        //Start the operation to load the Scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        //We loop here until the operation is done
        while (!operation.isDone)
            yield return null;

        //Indicate that the operation has been completed
        sceneLoadDone = true;
    }
}
