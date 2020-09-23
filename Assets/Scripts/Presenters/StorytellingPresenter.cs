using System.Collections.Generic;

/// <summary>
/// Custom <see cref="Presenter"/> class for <see cref="StorytellingView"/>.
/// </summary>
public class StorytellingPresenter : Presenter
{
    /// <summary>
    /// Reference to the view
    /// </summary>
    private StorytellingView _storytellingView;

    /// <summary>
    /// Reference to the model
    /// </summary>
    private StorytellingModel _storytellingModel;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="storytellingView">Reference for this class</param>
    public StorytellingPresenter(StorytellingView storytellingView)
    {
        _storytellingView = storytellingView;

        //Fetch the model instance
        _storytellingModel = StorytellingModel.GetInstance();

        //Fetch the content from the model
        string title = _storytellingModel.StoryTitle;
        string text = _storytellingModel.StoryText;

        //Apply content depending on if the story has a sprite or not
        if (_storytellingModel.StorySprite != null)
            _storytellingView.SetViewContent(title, _storytellingModel.StorySprite, text);
        else
            _storytellingView.SetViewContent(title, text);
    }

    /// <summary>
    /// Custom navigation method to navigate to the next view from the current <see cref="StorytellingView"/>
    /// </summary>
    /// <param name="storyNavigationPaths">List of valid storytelling navigation paths</param>
    public void OnContinue(List<StoryNavigationPath> storyNavigationPaths)
    {
        //Same type of system as used in the NavigationManager
        //Loop through valid paths
        foreach (StoryNavigationPath path in storyNavigationPaths)
        {
            //If the path status and the model status match i.e story outro
            if (path.StorytellingStatus == _storytellingModel.StorytellingStatus)
                //Start a navigation to the destination for this status
                NavigationInteraction(path.Destination);
        }
    }

    /// <summary>
    /// Custom navigation interaction method for <see cref="StorytellingView"/>
    /// </summary>
    /// <param name="destination">Destination view</param>
    private void NavigationInteraction(View destination)
    {
        NavigationManager.Instance.Navigate(destination.gameObject);
    }
}
