using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostersManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> allPosters;
    [SerializeField] private bool isWanted;

    private List<GameObject> remainingObjects;
    private int posterIndex;

    private void Awake()
    {
        if (isWanted)
        {
            remainingObjects = new List<GameObject>(allPosters);
            ActivateRandomWantedPosters(3);
        }
        else ActivateRandomPosters();
    }

    void ActivateRandomWantedPosters(int count)
    {
        if (count > remainingObjects.Count)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, remainingObjects.Count);
            remainingObjects[randomIndex].SetActive(true);
            remainingObjects.RemoveAt(randomIndex);
        }
    }

    public void ActivateRandomPosters()
    {
        foreach (GameObject obj in allPosters)
        {
            obj.SetActive(false);
        }
        posterIndex = Random.Range(0, allPosters.Count);
        allPosters[posterIndex].SetActive(true);
    }
}
