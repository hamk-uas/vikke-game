using UnityEngine;

/// <summary>
/// Custom <see cref="Presenter"/> for <see cref="LocationlistView"/>
/// </summary>
public class LocationlistPresenter : Presenter
{
    /// <summary>
    /// Reference to the <see cref="LocationlistView"/>
    /// </summary>
    private LocationlistView _locationlistView;

    /// <summary>
    /// Reference to the <see cref="StoryModel"/> class
    /// </summary>
    private StoryModel _storyModel;

    /// <summary>
    /// Reference to the currently active <see cref="Story"/>
    /// </summary>
    private Story _story;

    /// <summary>
    /// URL to Google Maps for navigation instructions
    /// </summary>
    private const string _gMapsUrl = "https://www.google.com/maps/search/?api=1&query=";
    
    /// <summary>
    /// Constructor for <see cref="LocationlistPresenter"/>
    /// </summary>
    /// <param name="locationlistView">Reference to the <see cref="LocationlistView"/></param>
    public LocationlistPresenter(LocationlistView locationlistView)
    {
        _locationlistView = locationlistView;

        //Fetch the StoryModel instance
        _storyModel = StoryModel.GetInstance();

        //Fetch a reference to the currenctly active Story
        _story = _storyModel.GetActiveStory();

        //Set the view content
        _locationlistView.SetViewTitle(_story.GetLocalizedTitle());
        _locationlistView.SetLocationsImage(_story.locationMapSprite);
        PopulateLocationList(_story);
    }

    /// <summary>
    /// Handle selection of a list item
    /// </summary>
    /// <param name="location">Selected <see cref="Location"/></param>
    public void OnLocationSelected(Location location)
    {
        //Make sure the Location has a Task set
        if (location.task)
        {
            //Create new LocatinModel for the selected Location
            new LocationModel(location);

            //Create a new StorytellingModel for displayning Story intro
            new StorytellingModel()
            {
                StorySprite = location.locationStorySprite,
                StorytellingStatus = StorytellingStatus.LOCATIONINTRO,
                StoryTitle = location.GetLocalizedTitle(),
                StoryText = location.GetLocalizedText()
            };

            //Navigate forwards in the application from this view
            NavigationInteraction(_locationlistView.gameObject, NavigationDirection.FORWARD);
        }
    }

    /// <summary>
    /// Populate the list of Locations displayed in the View
    /// </summary>
    /// <param name="story">Currently active <see cref="Story"/></param>
    private void PopulateLocationList(Story story)
    {
        foreach (Location location in story.locations)
        {
            //Fetch the completed state for the Location
            bool completed = PlayerDataManager.GetLocationState(location);

            //Check if the Location has a Task
            if (!location.task)
                //Spawn a disabled list item
                _locationlistView.SpawnDisabledLocationPrefab(location);
            else
                _locationlistView.SpawnLocationPrefab(location, completed);
        }
    }

    /// <summary>
    /// Handle selection of a list item's navigation direction element
    /// </summary>
    /// <param name="location"><see cref="Location"/> which navigation directions component was selected</param>
    public void OnLocationItemNavigationSelected(Location location)
    {
        //Create a Google Maps URL based on the Location's physical location
        string locationGMapsUrl = ConstructGMapsLink(location.latitude, location.longitude);
        //Open the URL in a browser
        Application.OpenURL(locationGMapsUrl);
    }

    /// <summary>
    /// Construct an URL for Google Maps
    /// </summary>
    /// <param name="lat">Latitude</param>
    /// <param name="lon">Longitude</param>
    /// <returns>Google Maps URL</returns>
    private string ConstructGMapsLink(float lat, float lon)
    {
        return _gMapsUrl + lat + "," + lon;
    }
}
