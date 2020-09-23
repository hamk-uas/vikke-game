using System.Collections.Generic;

/// <summary>
/// Custom <see cref="Presenter"/> for <see cref="StorylistView"/>
/// </summary>
public class StorylistPresenter : Presenter
{
    /// <summary>
    /// Reference to the <see cref="StorylistView"/>
    /// </summary>
    private StorylistView _storylistView;

    /// <summary>
    /// Constructor for <see cref="StorylistPresenter"/>
    /// </summary>
    /// <param name="storyList"></param>
    /// <param name="storylistView"></param>
    public StorylistPresenter(List<Story> storyList, StorylistView storylistView)
    {
        _storylistView = storylistView;
        PopulateStoryList(storyList);
    }

    /// <summary>
    /// Function intended to be called when the user has selected a <see cref="Story"/> from the list of stories displayed in <see cref="StorylistView"/>.
    /// <br />
    /// Creates required Models and makes a navigation interaction call to navigate to the <see cref="StorytellingView"/>.
    /// </summary>
    /// <param name="story">Selected <see cref="Story"/></param>
    public void OnStorySelected(Story story)
    {
        // Create new model for Story
        new StoryModel(story);

        // After the user has selected a Story, we want to display the Story intro.
        // Create a new Storytelling Model containing the Story intro content
        new StorytellingModel() {
            StorySprite = story.storytellingSprite,
            StorytellingStatus = StorytellingStatus.STORYINTRO,
            StoryTitle = story.GetLocalizedTitle(),
            StoryText = story.GetLocalizedIntroText()
        };

        // Indicate that we want to navigate forward in the application
        NavigationInteraction(_storylistView.gameObject, NavigationDirection.FORWARD);
    }

    /// <summary>
    /// Instantiate a list item for reach <see cref="Story"/> in <paramref name="storyList"/>
    /// </summary>
    /// <param name="storyList"><see cref="List{Story}"/> containing <see cref="Story"/> items to be instantiated</param>
    private void PopulateStoryList(List<Story> storyList)
    {
        foreach (Story story in storyList)
        {
            _storylistView.SpawnStoryPrefab(story);
        }
    }
}