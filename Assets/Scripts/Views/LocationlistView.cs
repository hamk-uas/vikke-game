using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Custom <see cref="View"/> class for listing <see cref="Location"/>s
/// </summary>
public class LocationlistView : View, IOnLocationItemSelect
{
    /// <summary>
    /// <see cref="GameObject"/> acting as the parent for instantiated <see cref="LocationlistItem"/>
    /// </summary>
    [SerializeField] private GameObject _itemContainer;

    /// <summary>
    /// Reference to the list item prefab
    /// </summary>
    [SerializeField] private LocationlistItem _locationlistItem;

    /// <summary>
    /// Title component in the view
    /// </summary>
    [SerializeField] private Text _storyTitle;

    /// <summary>
    /// Image component in the view
    /// </summary>
    [SerializeField] private Image _locationMapImage;

    /// <summary>
    /// <see cref="Color"/> that is used to indicate completed <see cref="Location"/>s
    /// </summary>
    [SerializeField] private Color completedColor;

    /// <summary>
    /// /// <see cref="Color"/> that is used to indicate incompleted <see cref="Location"/>s
    /// </summary>
    [SerializeField] private Color defaultColor;

    /// <summary>
    /// Width of the scroll container component in the view
    /// </summary>
    [SerializeField] private float _scrollContainerWidth;

    /// <summary>
    /// Reference for the custom <see cref="Presenter"/> for this view
    /// </summary>
    private LocationlistPresenter _locationlistPresenter;

    /// <summary>
    /// Called by Unity
    /// </summary>
    void Start()
    {
        SetPresenter(_locationlistPresenter = new LocationlistPresenter(this));
    }

    /// <summary>
    /// Set the content for the view title component
    /// </summary>
    /// <param name="title"></param>
    public void SetViewTitle(string title)
    {
        if(!string.IsNullOrEmpty(title))
            _storyTitle.text = title;
    }

    /// <summary>
    /// Set the content for the image component
    /// </summary>
    /// <param name="sprite"></param>
    public void SetLocationsImage(Sprite sprite)
    {
        if(sprite != null)
        {
            SetImageEnabled(true);
            _locationMapImage.sprite = sprite;
        }
        else
            SetImageEnabled(false);
            
    }

    /// <summary>
    /// Set the active state of the image element
    /// </summary>
    /// <param name="state"></param>
    private void SetImageEnabled(bool state)
    {
        _locationMapImage.gameObject.SetActive(state);
    }

    /// <summary>
    /// Spawn a list item to the item container
    /// </summary>
    /// <param name="location"><see cref="Location"/> that this item represents</param>
    /// <param name="completed">State of completion</param>
    public void SpawnLocationPrefab(Location location, bool completed)
    {
        //Instantiate a new list item to the container
        LocationlistItem item = Instantiate(_locationlistItem, _itemContainer.transform);
        //Set the item completion status based on the parameter
        if(completed)
            item.SetItemContent(location, this, completedColor);
        else
            item.SetItemContent(location, this, defaultColor);
    }

    /// <summary>
    /// Spawn a disabled (inactive) item to the item contaniner
    /// Used earlier when all Locations did not have a Task
    /// </summary>
    /// <param name="location"><see cref="Location"/> that this item represents</param>
    public void SpawnDisabledLocationPrefab(Location location)
    {
        LocationlistItem item = Instantiate(_locationlistItem, _itemContainer.transform);
        item.SetItemContent(location, this, Color.gray);
    }

    /// <summary>
    /// Callback for when a list item is selected
    /// </summary>
    /// <param name="location"><see cref="Location"/> which the item represents</param>
    public void OnLocationItemSelect(Location location)
    {
        _locationlistPresenter.OnLocationSelected(location);
    }

    /// <summary>
    /// Callback for when the navigation icon of a list item is selected
    /// </summary>
    /// <param name="location"><see cref="Location"/> which the item represents</param>
    public void OnLocationItemNavigateSelected(Location location)
    {
        _locationlistPresenter.OnLocationItemNavigationSelected(location);
    }
}