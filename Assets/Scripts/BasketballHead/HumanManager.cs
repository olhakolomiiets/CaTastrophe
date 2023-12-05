using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> allPeople;
    private int peopleIndex;
    private void Awake()
    {
        ActivateRandomHuman();
    }

    public void ActivateRandomHuman()
    {
        foreach (GameObject obj in allPeople)
        {
            obj.SetActive(false);
        }
        peopleIndex = Random.Range(0, allPeople.Count);
        allPeople[peopleIndex].SetActive(true);
    }
}
