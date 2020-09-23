using UnityEngine;

/// <summary>
/// Model class for Storytelling view content, singleton type
/// </summary>
public class StorytellingModel
{
    /* Because of limitations of the navigation "system" used in the application,
     * in order to reuse the StorytellingView prefab, 
     * we use this model to determine what content the view shows.
     * See comments in StorytellingView class for further information
     */

    /// <summary>
    /// Indicates the desired story state
    /// </summary>
    public StorytellingStatus StorytellingStatus { get; set; }

    /// <summary>
    /// Title for the story
    /// </summary>
    public string StoryTitle { get; set; }

    /// <summary>
    /// Image sprite for the story
    /// </summary>
    public Sprite StorySprite { get; set; }

    /// <summary>
    /// Text for the story
    /// </summary>
    public string StoryText { get; set; }

    /// <summary>
    /// Instance reference
    /// </summary>
    private static StorytellingModel Instance;

    /// <summary>
    /// Fetch the instance reference for the class
    /// </summary>
    /// <returns>Instance</returns>
    public static StorytellingModel GetInstance()
    {
        if (Instance == null)
            throw new System.InvalidOperationException("Instance not initialized");
        else
            return Instance;
    }

    /// <summary>
    /// Create new model instance
    /// </summary>
    public StorytellingModel()
    {
        Instance = this;
    }
}
