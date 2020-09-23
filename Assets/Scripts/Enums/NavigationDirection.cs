/// <summary>
/// Indicates a predetermined direction for a navigation interaction within the application
/// </summary>
[System.Serializable]
public enum NavigationDirection : short
{
    /// <summary>
    /// Navigate forward in the application
    /// </summary>
    FORWARD,

    /// <summary>
    /// Navigate back in the application
    /// </summary>
    BACK,

    /// <summary>
    /// Navigate to the AR scanner in the application
    /// </summary>
    SCANNER,

    /// <summary>
    /// Navigate to the story outro view in the application
    /// </summary>
    STORYEND,

    /// <summary>
    /// Navigate to the info view in the application
    /// </summary>
    INFO,
}
