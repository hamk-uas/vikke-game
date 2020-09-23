using UnityEngine;

/// <summary>
/// Container for navigation path in the application
/// </summary>
[System.Serializable]
public class NavigationPath
{
    /// <summary>
    /// The View prefab that called for navigation
    /// </summary>
    public GameObject CallerView;

    /// <summary>
    /// The direction for navigation
    /// </summary>
    public NavigationDirection Direction;

    /// <summary>
    /// The View prefab that is destination for this path
    /// </summary>
    public GameObject DestinationView;
}
