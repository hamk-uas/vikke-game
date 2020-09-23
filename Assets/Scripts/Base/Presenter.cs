using UnityEngine;

/// <summary>
/// Base class for Presenter in MVP model
/// </summary>
public class Presenter
{
    /// <summary>
    /// Calls <see cref="NavigationManager"/> indicating that a navigation interaction has been initiated
    /// </summary>
    /// <param name="gameObject"><see cref="GameObject"/> of the <see cref="View"/> making the call</param>
    /// <param name="component"><see cref="NavigationComponent"/> used to initiate the interaction</param>
    public virtual void NavigationInteraction(GameObject gameObject, NavigationComponent component)
    {
        NavigationManager.Instance.Navigate(gameObject, component.Direction);
    }

    /// <summary>
    /// Calls <see cref="NavigationManager"/> indicating that a navigation interaction has been initiated
    /// </summary>
    /// <param name="gameObject"><see cref="GameObject"/> of the <see cref="View"/> making the call</param>
    /// <param name="direction"><see cref="NavigationDirection"/> for the call</param>
    public virtual void NavigationInteraction(GameObject gameObject, NavigationDirection direction)
    {
        NavigationManager.Instance.Navigate(gameObject, direction);
    }
}
