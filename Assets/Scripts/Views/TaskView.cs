using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Custom <see cref="View"/> class for Task view. This view displays the chosen <see cref="Location"/>'s <see cref="Task"/> and handles user input for the task.
/// </summary>
public class TaskView : View
{
    /// <summary>
    /// Text element for displaying the title of the task
    /// </summary>
    [SerializeField] private Text _taskTitle;

    /// <summary>
    /// Image element for displaying the image for the task
    /// </summary>
    [SerializeField] private Image _taskImg;

    /// <summary>
    /// Text element for displaying the text of the task.
    /// <see cref="TMP_Text"/> in order to support interactable web links.
    /// </summary>
    [SerializeField] private TMP_Text _taskAssignment;

    /// <summary>
    /// Input field for user's answer to the task
    /// </summary>
    [SerializeField] private InputField _taskInput;

    /// <summary>
    /// Button which opens the AR Scanner scene
    /// </summary>
    [SerializeField] private Button _arScanner;

    /// <summary>
    /// Width of the of the parent container for the <see cref="Image"/> element, used to size the image correctly
    /// </summary>
    [SerializeField] private float _scrollContainerWidth;

    /// <summary>
    /// Custom <see cref="Presenter"/> for this View
    /// </summary>
    private TaskPresenter _taskPresenter;

    /// <summary>
    /// Called by Unity
    /// </summary>
    public void Start()
    {
        SetPresenter(_taskPresenter = new TaskPresenter(this));
    }

    /// <summary>
    /// Assign values for the display elements in the View
    /// </summary>
    /// <param name="taskTitle">Task title</param>
    /// <param name="taskImage">Task image as a <see cref="Sprite"/></param>
    /// <param name="taskAssignment">Task text</param>
    public void SetTaskContent(string taskTitle, Sprite taskImage, string taskAssignment)
    {
        _taskTitle.text = taskTitle;
        _taskAssignment.text = taskAssignment;
        _taskImg.sprite = taskImage;

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
        float aspectRatio = taskImage.rect.height / taskImage.rect.width;
        //Determine the correct height for the image based on the width of the parent element
        float preferredHeight = _scrollContainerWidth * aspectRatio;
        //Fetch a reference of the LayoutElement component
        LayoutElement layoutElement = _taskImg.GetComponent<LayoutElement>();
        //If the calculated height is smaller than the value set as default
        if (preferredHeight < layoutElement.preferredHeight)
            //Set the preferred height as the calculated value
            layoutElement.preferredHeight = preferredHeight;

        //Set the Image element as active
        SetTaskImageEnabled(true);
    }

    /// <summary>
    /// Assign values for the display elements in the View
    /// </summary>
    /// <param name="title">Task title</param>
    /// <param name="text">Tasj text</param>
    public void SetTaskContent(string taskTitle, string taskAssignment)
    {
        _taskTitle.text = taskTitle;
        _taskAssignment.text = taskAssignment;

        //No image for the task; disable the image element
        SetTaskImageEnabled(false);
    }

    /// <summary>
    /// Set the the Active state of the <see cref="Image"/> element in the View
    /// </summary>
    /// <param name="state">Active state of the element. True is active, false is disabled</param>
    public void SetTaskImageEnabled(bool state)
    {
        _taskImg.gameObject.SetActive(state);
    }

    /// <summary>
    /// Set the the Active state of the AR scanner <see cref="Button"/> element in the View
    /// </summary>
    /// <param name="state">Active state of the element. True is active, false is disabled</param>
    public void SetARScannerBtnActive(bool active)
    {
        _arScanner.interactable = active;
        _arScanner.gameObject.SetActive(active);
    }

    /// <summary>
    /// Passes the task text input for the answer to the <see cref="TaskPresenter"/>
    /// </summary>
    public void OnAnswerSubmit()
    {
        _taskPresenter.OnAnswerSubmit(_taskInput.text);
    }

    /// <summary>
    /// Calls <see cref="TaskPresenter"/> to open AR Scanner scene
    /// </summary>
    public void OnARScannerClick()
    {
        _taskPresenter.OpenARScanner();
    }

    /// <summary>
    /// Sets the task text input field according to the answer state
    /// </summary>
    /// <param name="correct">True turns the input field green; false red</param>
    public void SetAnswerStatus(bool correct)
    {
        if (correct)
            _taskInput.image.color = Color.green;
        else
            _taskInput.image.color = Color.red;
    }
}
