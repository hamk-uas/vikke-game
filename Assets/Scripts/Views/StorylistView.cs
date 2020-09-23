using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// View class for listing stories in the game
/// </summary>
public class StorylistView : View, IOnStoryItemSelected
{
    /// <summary>
    /// <see cref="GameObject"/> acting as the parent for instantiated <see cref="StorylistItem"/>s
    /// </summary>
    [SerializeField] private GameObject _itemContainer;

    /// <summary>
    /// Reference to the list item prefab
    /// </summary>
    [SerializeField] private StorylistItem _storylistItem;

    /// <summary>
    /// List of stories to be displayed
    /// </summary>
    [SerializeField] private List<Story> _storylist;

    /// <summary>
    /// Custom <see cref="Presenter"/> for this View
    /// </summary>
    private StorylistPresenter _storylistPresenter;

#if UNITY_EDITOR
    /// <summary>
    /// Construct a new <see cref="StorylistView"/>, used for testing
    /// </summary>
    /// <param name="itemcontainer"><see cref="GameObject"/> acting as the parent for instantiated <see cref="StorylistItem"/>s</param>
    /// <param name="storylistitem">Reference to the list item prefab</param>
    /// <param name="storylist">List of stories to be displayed</param>
    public void Construct(GameObject itemcontainer, StorylistItem storylistitem, List<Story> storylist)
    {
        _itemContainer = itemcontainer;
        _storylistItem = storylistitem;
        _storylist = storylist;
    }
#endif

    /// <summary>
    /// Called by Unity
    /// </summary>
    private void Start()
    {
        SetPresenter(_storylistPresenter = new StorylistPresenter(_storylist, this));
    }

    /// <summary>
    /// Instantiate a list item to the list of stories in the view
    /// </summary>
    /// <param name="story"><see cref="Story"/> for the item</param>
    public void SpawnStoryPrefab(Story story)
    {
        //Instantiate a new list item
        StorylistItem item = Instantiate(_storylistItem, _itemContainer.transform);
        //Set the item content
        item.SetItemContent(story, this);
    }

    public void OnStoryItemSelected(Story story)
    {
        _storylistPresenter.OnStorySelected(story);
    }
}