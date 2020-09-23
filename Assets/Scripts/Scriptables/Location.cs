using UnityEngine;
using Lean.Localization;

/// <summary>
/// Location content <see cref="ScriptableObject"/>
/// </summary>
[CreateAssetMenu(fileName = "Location", menuName = "Content Scriptables/Location", order = 1)]
public class Location : ScriptableObject
{
    /// <summary>
    /// Image for the location story
    /// </summary>
    public Sprite locationStorySprite;

    /// <summary>
    /// Task within the location
    /// </summary>
    public Task task;

    /// <summary>
    /// Physical address of the location
    /// </summary>
    public string address;

    /// <summary>
    /// Latitude and longitude of the location
    /// </summary>
    public float latitude, longitude;

    /// <summary>
    /// Finnish title for the location story
    /// </summary>
    public string fiTitle;

    /// <summary>
    /// Finnish place name i.e Hämeen Linna
    /// </summary>
    public string fiPlace;

    /// <summary>
    /// Finnish location story text
    /// </summary>
    [TextArea]
    public string fiText;

    /// <summary>
    /// English title for the location story
    /// </summary>
    public string enTitle;

    /// <summary>
    /// English place name i.e Häme Castle
    /// </summary>
    public string enPlace;

    /// <summary>
    /// English location story text
    /// </summary>
    [TextArea]
    public string enText;

    /// <summary>
    /// Fetch <see cref="Location"/> story title in currently set language
    /// </summary>
    /// <returns>Localized story title</returns>
    public string GetLocalizedTitle()
    {
        switch (LeanLocalization.CurrentLanguage)
        {
            case "English":
                return enTitle;

            case "Finnish":
                return fiTitle;

            default:
                return enTitle;
        }
    }

    /// <summary>
    /// Fetch <see cref="Location"/> place name in currently set language
    /// </summary>
    /// <returns>Localized place name</returns>
    public string GetLocalizedPlace()
    {
        switch (LeanLocalization.CurrentLanguage)
        {
            case "English":
                return enPlace;

            case "Finnish":
                return fiPlace;

            default:
                return enPlace;
        }
    }

    /// <summary>
    /// Fetch <see cref="Location"/> story text in currently set language
    /// </summary>
    /// <returns>Localized story text</returns>
    public string GetLocalizedText()
    {
        switch (LeanLocalization.CurrentLanguage)
        {
            case "English":
                return enText;

            case "Finnish":
                return fiText;

            default:
                return enText;
        }
    }
}
