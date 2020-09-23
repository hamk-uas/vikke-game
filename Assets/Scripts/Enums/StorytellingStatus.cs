/// <summary>
/// Indicates the current story state for the Storytelling view.
/// </summary>
[System.Serializable]
public enum StorytellingStatus : short
{
    /// <summary>
    /// Indicate that the intro for the <see cref="Story"/> should be shown
    /// </summary>
    STORYINTRO,

    /// <summary>
    /// Indicate that the outro for the <see cref="Story"/> should be shown
    /// </summary>
    STORYOUTRO,

    /// <summary>
    /// Indicate that the story for the <see cref="Location"/> should be shown
    /// </summary>
    LOCATIONINTRO,
}
