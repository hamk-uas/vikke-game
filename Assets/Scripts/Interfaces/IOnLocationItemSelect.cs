/// <summary>
/// Interface for location list item selection
/// </summary>
public interface IOnLocationItemSelect
{
    /// <summary>
    /// Callback for when a list item is selected
    /// </summary>
    /// <param name="location"></param>
    void OnLocationItemSelect(Location location);

    /// <summary>
    /// Callback for when a list item's navigation directions element is selected
    /// </summary>
    /// <param name="location"></param>
    void OnLocationItemNavigateSelected(Location location);

    /// <summary>
    /// Callback for instantiating a new list item
    /// </summary>
    /// <param name="location"><see cref="Location"/> which this list item represents</param>
    /// <param name="completed">Completion status of this list item</param>
    void SpawnLocationPrefab(Location location, bool completed);
}
