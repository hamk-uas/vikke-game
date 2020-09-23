/// <summary>
/// Interface for story list item selection
/// </summary>
public interface IOnStoryItemSelected
{
    /// <summary>
    /// Callback for when a story list item is selected
    /// </summary>
    /// <param name="story">Selected <see cref="Story"/></param>
    void OnStoryItemSelected(Story story);
}
