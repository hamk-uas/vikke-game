using UnityEngine;

/// <summary>
/// Base class for View in MVP model
/// </summary>
public class View : MonoBehaviour
{
    /// <summary>
    /// Presenter for the <see cref="View"/>
    /// </summary>
    private Presenter presenter;

    /// <summary>
    /// Called by Unity
    /// </summary>
    public virtual void Awake()
    {
        //Create a new instance on Awake
        presenter = new Presenter();
    }

    /// <summary>
    /// Called by Unity on each frame
    /// </summary>
    public virtual void Update()
    {
        //Check if "Back" key has been pressed
        if (Input.GetKey(KeyCode.Escape))
        {
            //Initiate a navigation interaction indicating that the user wishes to navigate back in the application
            NavigationInteraction(NavigationDirection.BACK);
        }
    }

    /// <summary>
    /// Set a new Presenter for the <see cref="View"/>
    /// </summary>
    /// <param name="presenter">New <see cref="Presenter"/> for the <see cref="View"/></param>
    protected void SetPresenter(Presenter presenter)
    {
        this.presenter = presenter;
    }

    /// <summary>
    /// Fetch a reference to the <see cref="Presenter"/> set for this <see cref="View"/>
    /// </summary>
    /// <returns>Reference of the currently set <see cref="Presenter"/></returns>
    public Presenter GetPresenter()
    {
        return presenter;
    }

    /// <summary>
    /// Initiate a navigation interaction in the <see cref="NavigationDirection"/> given as parameter
    /// </summary>
    /// <param name="direction"><see cref="NavigationDirection"/> for the interaction</param>
    private void NavigationInteraction(NavigationDirection direction)
    {
        presenter.NavigationInteraction(gameObject, direction);
    }

    /// <summary>
    /// Pass a navigation interaction with a reference to a <see cref="NavigationComponent"/> initiating the interaction
    /// </summary>
    /// <param name="button"><see cref="NavigationComponent"/> initiating the interaction</param>
    public void NavigationInteraction(NavigationComponent button)
    {
        presenter.NavigationInteraction(gameObject, button);
    }
}
