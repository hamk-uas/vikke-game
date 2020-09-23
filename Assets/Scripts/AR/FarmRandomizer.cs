using UnityEngine;

/// <summary>
/// Randomize visibility of farm animals in Vasikanjuottaja AR content
/// </summary>
public class FarmRandomizer : MonoBehaviour
{
    [SerializeField] private GameObject _chicken;
    [SerializeField] private GameObject _chicken1;
    [SerializeField] private GameObject _chicken2;
    [SerializeField] private GameObject _pig;
    [SerializeField] private GameObject _pig1;
    [SerializeField] private GameObject _sheep;
    [SerializeField] private GameObject _sheep1;

    void Start()
    {
        int random = Random.Range(1, 3);

        //30% chance for each chicken to be disabled
        if (random == 1)
            _chicken.SetActive(false);
        else if (random == 2)
            _chicken1.SetActive(false);
        else if (random == 3)
            _chicken2.SetActive(false);

        random = Random.Range(1, 10);

        //40% chance to switch a pig to a sheep
        if (random >= 7)
        {
            _pig.SetActive(false);
            _sheep1.SetActive(true);
        }

        //20% chance to switch a sheep to a pig
        if (random >= 9)
        {
            _sheep.SetActive(false);
            _pig1.SetActive(true);
        }
        
    }
}
