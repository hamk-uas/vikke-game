using UnityEngine;
using UnityEngine.UI;

public class CreditsView : View
{
    /// <summary>
    /// Text for displaying the version information
    /// </summary>
    [SerializeField]
    private Text _versionInfo;

    /// <summary>
    /// Called by Unity
    /// </summary>
    void Start()
    {
        //Display the application version
        _versionInfo.text = $"V {Application.version}";
    }
}
