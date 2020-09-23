using UnityEngine;

/// <summary>
/// This script can be used as a component to define a <see cref="NavigationDirection"/> for an UI element calling for a navigation interaction in the application.
/// <br /><br />
/// Attach this script as a child to an UI element, and use it to define the <see cref="NavigationDirection"/> for the navigation interaction.
/// <br />
/// The <see cref="NavigationDirection"/> defined with this component is passed along the <see cref="View.NavigationInteraction(NavigationComponent)"/> call.
/// <br /><br />
/// This is a workaround to pass a custom parameter along a <see cref="UnityEngine.UI.Button.onClick"/> event.
/// </summary>
public class NavigationComponent : MonoBehaviour
{
    /// <summary>
    /// <inheritdoc cref="NavigationDirection"/>
    /// </summary>
    public NavigationDirection Direction;
}
