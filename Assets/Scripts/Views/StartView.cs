using Lean.Localization;
using UnityEngine;

/// <summary>
/// The initial view for the application
/// </summary>
public class StartView : View
{
    /// <summary>
    /// URL for feedback survey in Finnish
    /// </summary>
    [SerializeField] private string _fiFeedbackUrl;
    /// <summary>
    /// /// URL for feedback survey in English
    /// </summary>
    [SerializeField] private string _enFeedbackUrl;

    /// <summary>
    /// Set the display language of the application
    /// </summary>
    /// <param name="language">Name of the language, eg. Finnish</param>
    public void SetLanguage(string language)
    {
        //Check if the parameter matches the currently set language
        if (LeanLocalization.CurrentLanguage != language)
            LeanLocalization.CurrentLanguage = language;
    }

    /// <summary>
    /// Open feedback query in a browser based on the current display language
    /// </summary>
    public void OpenFeedback()
    {
        if (LeanLocalization.CurrentLanguage == "Finnish")
            Application.OpenURL(_fiFeedbackUrl);
        else if (LeanLocalization.CurrentLanguage == "English")
            Application.OpenURL(_enFeedbackUrl);
    }
}
