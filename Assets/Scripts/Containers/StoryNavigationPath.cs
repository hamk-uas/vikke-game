/// <summary>
/// Container for a navigation path from the Storytelling View in the application
/// </summary>
[System.Serializable]
public class StoryNavigationPath
{
    /// <summary>
    /// Destination view where this path should take
    /// </summary>
    public View Destination;

    /// <summary>
    /// Indicate the status when this path should be taken
    /// </summary>
    public StorytellingStatus StorytellingStatus;
}
