using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Custom <see cref="View"/> class for the Storytelling view. Used to display story content to the user.
/// <br/>
/// Navigation to and from this View is differernt from the rest of the Views in the app.
/// <br/>
/// See comments in this class for further information.
/// </summary>
public class StorytellingView : View
{
    /// <summary>
    /// Text element for displaying the title of the story
    /// </summary>
    [SerializeField] private Text _storytellingTitle;

    /// <summary>
    /// Image element for displaying the image for the story
    /// </summary>
    [SerializeField] private Image _storytellingImg;

    /// <summary>
    /// Text element for displaying the text of the story 
    /// </summary>
    [SerializeField] private Text _storytellingText;

    /// <summary>
    /// Width of the of the parent container for the <see cref="Image"/> element, used to size the image correctly
    /// </summary>
    [SerializeField] private float _scrollContainerWidth;

    /* 
     * This view has it's own list of navigation paths, as the app's navigation "system" is linear and
     * does not work if there are multiple possible paths starting from one view.
     * The system has no possibility of distinguishing between multiple different paths from a view.
     * Other option would've been to have have a view prefab for each
     * storytelling "step" (story intro, location story, and story outro)
     * but I decided to solve this programmatically.
    */
    /// <summary>
    /// List of possible <see cref="StoryNavigationPath"/>s used to navigate out from this view
    /// </summary>
    [SerializeField] private List<StoryNavigationPath> _navigationPaths;

    /// <summary>
    /// Custom <see cref="Presenter"/> for this View
    /// </summary>
    private StorytellingPresenter _storytellingPresenter;

    /// <summary>
    /// Called by Unity
    /// </summary>
    public void Start()
    {
        SetPresenter(_storytellingPresenter = new StorytellingPresenter(this));
    }

    /// <summary>
    /// Assign values for the display elements in the View
    /// </summary>
    /// <param name="title">Story title</param>
    /// <param name="image">Story image as a <see cref="Sprite"/></param>
    /// <param name="text">Story text</param>
    public void SetViewContent(string title, Sprite image, string text)
    {
        _storytellingTitle.text = title;
        _storytellingText.text = text;
        _storytellingImg.sprite = image;

        /*
         * Vertical Layout Group doesn't resize images that are wider than the parent correctly according to
         * their aspect ratio. The height of the element is set as the height of the source image,
         * but the width is set according to the parent element. This results in images being
         * streched, or if "Preserve Aspect" is set to true, there's empty space around the image.
         * This is why we do the resizing manually, using LayoutElement to set the preferred height.
         * The parent element has a Content Size Fitter with "Vertical Fit" set to "Preferred Size"
         * in order for the parent element to respect the height we set here.
        */
        //Calculate the aspect ratio
        float aspectRatio = image.rect.height / image.rect.width;
        //Determine the correct height for the image based on the width of the parent element
        float preferredHeight = _scrollContainerWidth * aspectRatio;
        //Fetch a reference of the LayoutElement component
        LayoutElement layoutElement = _storytellingImg.GetComponent<LayoutElement>();
        //If the calculated height is smaller than the value set as default
        if (preferredHeight < layoutElement.preferredHeight)
            //Set the preferred height as the calculated value
            layoutElement.preferredHeight = preferredHeight;

        //Set the Image element as active
        SetViewImageEnabled(true);
    }

    /// <summary>
    /// Assign values for the display elements in the View
    /// </summary>
    /// <param name="title">Story title</param>
    /// <param name="text">Story text</param>
    public void SetViewContent(string title, string text)
    {
        _storytellingTitle.text = title;
        _storytellingText.text = text;

        //No image for the story; disable the image element
        SetViewImageEnabled(false);
    }

    /// <summary>
    /// Set the the Active state of the <see cref="Image"/> element in the View
    /// </summary>
    /// <param name="state">Active state of the element. True is active, false is disabled</param>
    public void SetViewImageEnabled(bool state)
    {
        _storytellingImg.gameObject.SetActive(state);
    }

    /// <summary>
    /// Indicate the user has given input to navigate out from the current view.
    /// Pass the list of possible <see cref="StoryNavigationPath"/>s to the Presenter
    /// </summary>
    public void OnContinue()
    {
        _storytellingPresenter.OnContinue(_navigationPaths);
    }
}