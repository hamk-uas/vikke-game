using Lean.Localization;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Story content <see cref="ScriptableObject"/>
/// </summary>
[CreateAssetMenu(fileName = "Story", menuName = "Content Scriptables/Story", order = 0)]
public class Story : ScriptableObject
{
    /// <summary>
    /// Image for the storytelling view
    /// </summary>
    public Sprite storytellingSprite;

    /// <summary>
    /// Map image shown in the location list
    /// </summary>
    public Sprite locationMapSprite;

    /// <summary>
    /// List of locations in the story
    /// </summary>
    public List<Location> locations;

    /// <summary>
    /// Finnish story title
    /// </summary>
    public string fiTitle;

    /// <summary>
    /// Finnish story description
    /// </summary>
    public string fiDesc;

    /// <summary>
    /// Finnish story intro text
    /// </summary>
    [TextArea]
    public string fiIntroText;

    /// <summary>
    /// Finnish story outro text
    /// </summary>
    [TextArea]
    public string fiOutroText;

    /// <summary>
    /// English story title
    /// </summary>
    public string enTitle;

    /// <summary>
    /// English story description
    /// </summary>
    public string enDesc;

    /// <summary>
    /// English story intro text
    /// </summary>
    [TextArea]
    public string enIntroText;

    /// <summary>
    /// English outro text
    /// </summary>
    [TextArea]
    public string enOutroText;

    /// <summary>
    /// Fetch <see cref="Story"/> title in currently set language
    /// </summary>
    /// <returns>Localized title</returns>
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
    /// Fetch <see cref="Story"/> description in currently set language
    /// </summary>
    /// <returns>Localized description</returns>
    public string GetLocalizedDesc()
    {
        switch (LeanLocalization.CurrentLanguage)
        {
            case "English":
                return enDesc;

            case "Finnish":
                return fiDesc;

            default:
                return enDesc;
        }
    }

    /// <summary>
    /// Fetch <see cref="Story"/> intro text in currently set language
    /// </summary>
    /// <returns>Localized intro text</returns>
    public string GetLocalizedIntroText()
    {
        switch (LeanLocalization.CurrentLanguage)
        {
            case "English":
                return enIntroText;

            case "Finnish":
                return fiIntroText;

            default:
                return enIntroText;
        }
    }

    /// <summary>
    /// Fetch <see cref="Story"/> outro text in currently set language
    /// </summary>
    /// <returns>Localized outro text</returns>
    public string GetLocalizedOutroText()
    {
        switch (LeanLocalization.CurrentLanguage)
        {
            case "English":
                return enOutroText;

            case "Finnish":
                return fiOutroText;

            default:
                return enOutroText;
        }
    }

    /// <summary>
    /// Fetch <see cref="Story"/> story finished title in currently set language
    /// </summary>
    /// <returns>Localized story finished title</returns>
    public string GetLocalizedStoryFinishTitle()
    {
        switch (LeanLocalization.CurrentLanguage)
        {
            //TODO: Move hardcoded translation to LeanLocalization
            case "English":
                return "Story completed!";

            case "Finnish":
                return "Tarina suoritettu!";

            default:
                return "Story completed!";
        }
    }
}
