/// <summary>
/// Model class for Story selected by user, singleton type
/// </summary>
public class LocationModel
{
    //Current active location
    private Location _activeLocation;
    //Instance of the current LocationModel
    private static LocationModel _instance;

    /// <summary>
    /// Fetch instance of the <see cref="LocationModel"/>
    /// </summary>
    /// <returns>Instance of <see cref="LocationModel"/></returns>
    public static LocationModel GetInstance()
    {
        if (_instance == null)
            throw new System.InvalidOperationException("Instance not initialized");
        else
            return _instance;
    }

    /// <summary>
    /// Constructor for the Model
    /// </summary>
    /// <param name="story">Active <see cref="Location"/></param>
    public LocationModel(Location location)
    {
        _activeLocation = location;
        _instance = this;
    }

    /// <summary>
    /// Set new active <see cref="Location"/>
    /// </summary>
    /// <param name="story">New active <see cref="Location"/></param>
    public void SetActiveLocation(Location location)
    {
        _activeLocation = location;
    }

    /// <summary>
    /// Fetch the current active <see cref="Location"/>
    /// </summary>
    /// <returns>Current active <see cref="Location"/></returns>
    public Location GetActiveLocation()
    {
        return _activeLocation;
    }

    /// <summary>
    /// Fetch the <see cref="Task"/> for the current active <see cref="Location"/>
    /// </summary>
    /// <returns><see cref="Task"/> for the current active <see cref="Location"/></returns>
    public Task GetLocationTask()
    {
        return _activeLocation.task;
    }

    /// <summary>
    /// Set and save the completion status for the current active <see cref="Location"/>
    /// </summary>
    /// <param name="completed">True indicates that the <see cref="Location"/> has been completed; False that it is not completed</param>
    public void SetCompletionStatus(bool completed)
    {
        PlayerDataManager.SaveLocationState(_activeLocation, completed);
    }
}
