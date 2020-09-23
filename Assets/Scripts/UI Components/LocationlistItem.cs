using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script class for list items in <see cref="LocationlistView"/>
/// </summary>
public class LocationlistItem : MonoBehaviour
{
    /// <summary>
    /// References to the text components in the item
    /// </summary>
    [SerializeField] private Text _title, _desc, _address;

    /// <summary>
    /// Reference to the image component in the item
    /// </summary>
    [SerializeField] private Image _image;

    /// <summary>
    /// Reference to a <see cref="Location"/> for this item
    /// </summary>
    private Location _location;

    /// <summary>
    /// Callback used to register a user interaction
    /// </summary>
    private IOnLocationItemSelect _callback;

#if UNITY_EDITOR
    /// <summary>
    /// Constructor for testing
    /// </summary>
    /// <param name="title">Title component</param>
    /// <param name="desc">Description component</param>
    public void Constructor(Text title, Text desc)
    {
        _title = title;
        _desc = desc;
    }
#endif

    /// <summary>
    /// Set content for this item
    /// </summary>
    /// <param name="location"><see cref="Location"/> reference for this item</param>
    /// <param name="callback">Callback interface used to register user interactions</param>
    /// <param name="itemBackground"><see cref="Color"/></param> used as the background for this item
    public void SetItemContent(Location location, IOnLocationItemSelect callback, Color itemBackground)
    {
        _title.text = location.GetLocalizedTitle();
        _desc.text = location.GetLocalizedPlace();
        _address.text = location.address;
        _location = location;
        _callback = callback;
        _image.color = itemBackground;
    }

    /// <summary>
    /// Register that user has selected this item
    /// </summary>
    public void ItemSelected()
    {
        _callback.OnLocationItemSelect(_location);
    }

    /// <summary>
    /// Register that user has selected the navigation item of this item
    /// </summary>
    public void ItemNavigateSelected()
    {
        _callback.OnLocationItemNavigateSelected(_location);
    }
}
