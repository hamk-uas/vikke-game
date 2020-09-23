using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script class for list items in <see cref="StorylistView"/>
/// </summary>
public class StorylistItem : MonoBehaviour
{
    /// <summary>
    /// Text fields for item content
    /// </summary>
    [SerializeField] private Text _title, _desc;

    /// <summary>
    /// Reference for the <see cref="Story"/> this item represents
    /// </summary>
    private Story _story;

    /// <summary>
    /// Callback for when the item is selected
    /// </summary>
    private IOnStoryItemSelected _callback;

#if UNITY_EDITOR
    /// <summary>
    /// Construct a new <see cref="StorylistItem"/>, used for testing
    /// </summary>
    /// <param name="title"></param>
    /// <param name="desc"></param>
    public void Constructor(Text title, Text desc)
    {
        _title = title;
        _desc = desc;
    }
#endif

    /// <summary>
    /// Set content for this list item
    /// </summary>
    /// <param name="story"><see cref="Story"/> this item represents</param>
    /// <param name="callback">Callback for then the item is selected</param>
    public void SetItemContent(Story story, IOnStoryItemSelected callback)
    {
        _title.text = story.GetLocalizedTitle();
        _desc.text = story.GetLocalizedDesc();
        _callback = callback;
        _story = story;
    }

    /// <summary>
    /// Function intended to be linked to a <see cref="Button.onClick"/> event,
    /// so that this function is called when the user selects the item.
    /// </summary>
    public void ItemSelected()
    {
        _callback.OnStoryItemSelected(_story);
    }
}