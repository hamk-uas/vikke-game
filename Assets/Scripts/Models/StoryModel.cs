using System.Collections.Generic;

/// <summary>
/// Model class for Story selected by user, singleton type
/// </summary>
public class StoryModel
{
    /// <summary>
    /// Current active story
    /// </summary>
    private Story _activeStory;

    /// <summary>
    /// Instance of the current StoryModel
    /// </summary>
    private static StoryModel Instance;

    /// <summary>
    /// Fetch instance of the <see cref="StoryModel"/>
    /// </summary>
    /// <returns>Instance of <see cref="StoryModel"/></returns>
    public static StoryModel GetInstance()
    {
        if (Instance == null)
            throw new System.InvalidOperationException("Instance not initialized");
        else
            return Instance;
    }

    /// <summary>
    /// Constructor for the Model
    /// </summary>
    /// <param name="story">Active <see cref="Story"/></param>
    public StoryModel(Story story)
    {
        _activeStory = story;
        Instance = this;
    }

    /// <summary>
    /// Set new active <see cref="Story"/>
    /// </summary>
    /// <param name="story">New active <see cref="Story"/></param>
    public void SetActiveStory(Story story)
    {
        _activeStory = story;
    }

    /// <summary>
    /// Fetch the current active <see cref="Story"/>
    /// </summary>
    /// <returns>Current active <see cref="Story"/></returns>
    public Story GetActiveStory()
    {
        return _activeStory;
    }

    /// <summary>
    /// Fetch a list containing the <see cref="Location"/>s for the <see cref="Story"/>
    /// </summary>
    /// <returns>List of <see cref="Location"/>s</returns>
    public List<Location> GetStoryLocations()
    {
        return _activeStory.locations;
    }

    /// <summary>
    /// Checks if all <see cref="Location"/>s in the current active <see cref="Story"/> are completed
    /// </summary>
    /// <returns>True if all locations are completed; otherwise false</returns>
    public bool AllLocationsCompleted()
    {
        //Go through each Location in the story
        foreach (Location location in GetStoryLocations())
        {
            //If Location completion state is FALSE = not complete
            if (!PlayerDataManager.GetLocationState(location))
                //Return false
                return false;
        }
        //None of the Location had their completion state set to FALSE,
        //Return true
        return true;
    }

    /// <summary>
    /// Set and save the completion status for the current active <see cref="Story"/>
    /// </summary>
    /// <param name="completed">True indicates that the <see cref="Story"/> has been completed; False that it is not completed</param>
    public void SetCompletionStatus(bool completed)
    {
        PlayerDataManager.SaveStoryState(_activeStory, completed);
    }
}
