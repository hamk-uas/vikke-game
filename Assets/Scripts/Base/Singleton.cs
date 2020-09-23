using UnityEngine;

/// <summary>
/// A base class for any Singleton. 
/// Provides access to a MonoBehaviour.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    private static bool _applicationIsQuitting = false;

    /// <summary>
    /// Indicates whether this instance is destroyed.
    /// </summary>
    public static bool IsDestoyed { get { return _instance == null; } }

    /// <summary>
    /// Retuns the instance. 
    /// Creates new instance if not currently part of the scene.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_applicationIsQuitting)
            {
                return null;
            }

            if (!_instance)
            {
                _instance = FindObjectOfType<T>();

                if (!_instance)
                {
                    // Components which inherit MonoBehaviour must be added to the scene
                    _instance = new GameObject().AddComponent<T>();
                    _instance.gameObject.name = _instance.GetType().Name;
                }
            }
            return _instance;
        }
    }


    private void Awake()
    {
        if(_instance == null)
            _instance = FindObjectOfType<T>();
        else
            Destroy(gameObject);            

        _applicationIsQuitting = false;
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    private void OnApplicationQuit()
    {
        _instance = null;
        _applicationIsQuitting = true;
    }
}
