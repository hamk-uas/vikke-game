using UnityEngine;

/// <summary>
/// Static class for handling <see cref="Story"/> and <see cref="Location"/> completion status
/// </summary>
public static class PlayerDataManager
{
    /// <summary>
    /// Save completion status for a <see cref="Story"/>
    /// </summary>
    /// <param name="story"></param>
    /// <param name="completed">True for completed, false for incomplete</param>
    public static void SaveStoryState(Story story, bool completed)
    {
        SaveBool(story.name, completed);
    }

    /// <summary>
    /// Load the completion status for a <see cref="Story"/>
    /// </summary>
    /// <param name="story"></param>
    /// <returns>True for completed, false for incomplete</returns>
    public static bool GetStoryState(Story story)
    {
        return GetBool(story.name);
    }

    /// <summary>
    /// Save completion status for  a <see cref="Location"/>
    /// </summary>
    /// <param name="location"></param>
    /// <param name="completed">True for completed, false for incomplete</param>
    public static void SaveLocationState(Location location, bool completed)
    {
        SaveBool(location.name, completed);
    }

    /// <summary>
    /// Load the completion status for a <see cref="Location"/>
    /// </summary>
    /// <param name="location"></param>
    /// <returns>True for completed, false for incomplete</returns>
    public static bool GetLocationState(Location location)
    {
        return GetBool(location.name);
    }

    /// <summary>
    /// Save a key with a given boolean value
    /// </summary>
    /// <param name="key">Key for storing the value</param>
    /// <param name="state">Value to be stored</param>
    private static void SaveBool(string key, bool state)
    {
        if (state)
            PlayerPrefs.SetInt(key, 1);
        else
            PlayerPrefs.SetInt(key, 0);
    }

    /// <summary>
    /// Load the value of a given key
    /// </summary>
    /// <param name="key">Key for fetching the stored data</param>
    /// <returns>True if stored value is 1, otherwise false</returns>
    private static bool GetBool(string key)
    {
        if (PlayerPrefs.GetInt(key) == 1)
            return true;
        else
            return false;
    }
}
