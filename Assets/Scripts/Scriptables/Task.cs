using Lean.Localization;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Task content <see cref="ScriptableObject"/>
/// </summary>
[CreateAssetMenu(fileName = "Task", menuName = "Content Scriptables/Task", order = 3)]
public class Task : ScriptableObject
{
    /// <summary>
    /// Optional image shown in the task assignment
    /// </summary>
    public Sprite taskviewSprite;

    /// <summary>
    /// Correct answers to the task
    /// </summary>
    public List<string> answers;

    /// <summary>
    /// Does the task require using the AR scanner
    /// </summary>
    public bool isARTask;

    /// <summary>
    /// Finnish title for the task
    /// </summary>
    public string fiTitle;

    /// <summary>
    /// Finnish text for the task
    /// </summary>
    [TextArea]
    public string fiText;

    /// <summary>
    /// Finnish title for the task
    /// </summary>
    public string enTitle;

    /// <summary>
    /// Finnish text for the task
    /// </summary>
    [TextArea]
    public string enText;

    /// <summary>
    /// Fetch <see cref="Task"/> title in currently set language
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
    /// Fetch <see cref="Task"/> text in currently set language
    /// </summary>
    /// <returns>Localized text</returns>
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