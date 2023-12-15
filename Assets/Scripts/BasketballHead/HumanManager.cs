using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> allPeople;
    private int peopleIndex;
    private void Awake()
    {
        peopleIndex = Random.Range(0, allPeople.Count);
        ActivateRandomHuman();
    }

    public void ActivateRandomHuman()
    {
        foreach (GameObject obj in allPeople)
        {
            obj.SetActive(false);
        }
        
        allPeople[peopleIndex].SetActive(true);
    }

    public void DisableHuman()
    {
        allPeople[peopleIndex].SetActive(false);
    }
}
