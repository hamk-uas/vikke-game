/// <summary>
/// Custom <see cref="Presenter"/> class for <see cref="TaskView"/>
/// </summary>
public class TaskPresenter : Presenter
{
    /// <summary>
    /// Reference to the view
    /// </summary>
    private TaskView _taskView;

    /// <summary>
    /// Reference to a <see cref="LocationModel"/>
    /// </summary>
    private LocationModel _locationModel;

    /// <summary>
    /// Reference to a <see cref="StoryModel"/>
    /// </summary>
    private StoryModel _storyModel;

    /// <summary>
    /// Object for the currently active <see cref="Story"/>
    /// </summary>
    private Story _activeStory;

    /// <summary>
    /// Object for the currently active <see cref="Task"/>
    /// </summary>
    private Task _task;

    /// <summary>
    /// Defauly constructor
    /// </summary>
    /// <param name="taskView">Reference of the view for this class</param>
    public TaskPresenter(TaskView taskView)
    {
        _taskView = taskView;

        //Fetch the model instances
        _locationModel = LocationModel.GetInstance();
        _storyModel = StoryModel.GetInstance();
        //Get the active story
        _activeStory = _storyModel.GetActiveStory();
        //Get the active task
        _task = _locationModel.GetLocationTask();

        //Apply image content depending on of the task has an image or not
        if (_task.taskviewSprite != null)
            _taskView.SetTaskContent(_task.GetLocalizedTitle(), _task.taskviewSprite, _task.GetLocalizedText());
        else
            _taskView.SetTaskContent(_task.GetLocalizedTitle(), _task.GetLocalizedText());

        //Display the AR scanner button if the task has AR content
        _taskView.SetARScannerBtnActive(_task.isARTask);
    }

    /// <summary>
    /// Check the input that the user has provided, and determine action based on the current story status
    /// </summary>
    /// <param name="userInput">User input from a text field</param>
    public void OnAnswerSubmit(string userInput)
    {
        //Check if the input has any content
        if (!string.IsNullOrEmpty(userInput) && !string.IsNullOrWhiteSpace(userInput))
            //Chec if the input matches task answers
            if (IsInputCorrectAnswer(userInput))
            {
                _taskView.SetAnswerStatus(true);
                //Set the location completed
                _locationModel.SetCompletionStatus(true);
                //Check if all location within the story have been completed
                if (_storyModel.AllLocationsCompleted())
                {
                    //All locations completed -> set story as completed
                    _storyModel.SetCompletionStatus(true);

                    //Create new model for the StorytellingView
                    new StorytellingModel()
                    {
                        StorySprite = _activeStory.storytellingSprite,
                        StorytellingStatus = StorytellingStatus.STORYOUTRO,
                        StoryTitle = _activeStory.GetLocalizedStoryFinishTitle(),
                        StoryText = _activeStory.GetLocalizedOutroText()
                    };

                    //New navigation interaction with direction to story ending
                    NavigationInteraction(_taskView.gameObject, NavigationDirection.STORYEND);
                }
                else
                    //Not all locations were completed, navigate forward from this view
                    NavigationInteraction(_taskView.gameObject, NavigationDirection.FORWARD);
            }
            else
                //User input did not match any answer
                _taskView.SetAnswerStatus(false);
    }

    /// <summary>
    /// Check if input matches any answers in the task
    /// </summary>
    /// <param name="userInput">User input</param>
    /// <returns></returns>
    private bool IsInputCorrectAnswer(string userInput)
    {
        //Make sure the whole input is in lower case
        string userInputLowerCase = userInput.ToLower();

        foreach (string correctAnswer in _task.answers)
        {
            //Make sure the answer is in lower case too
            string correctAnswerLowercase = correctAnswer.ToLower();
            if (userInputLowerCase == correctAnswerLowercase)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Open the AR Scene for viewing AR content
    /// </summary>
    public void OpenARScanner()
    {
        //Fetch the NavigationManager instance and call to open the AR Scene for viewing AR content
        NavigationManager.Instance.OpenARScene();
    }
}
