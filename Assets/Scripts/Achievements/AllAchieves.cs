using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAchieves : MonoBehaviour
{
    private List<Transform> allAchieves = new List<Transform>();
    public GameObject _Achieves;

    void Start()
    {
        foreach (Transform child in _Achieves.transform)
        {
            allAchieves.Add(child);
        }
        foreach (var x in allAchieves)
        {
            Debug.Log(x.ToString());
        }
    }
    public void checkAchieve()
    {
        for (int i = 0; i < allAchieves.Count; i++)
        {
            if (allAchieves[i].GetComponent<Achieve>().realNumber > allAchieves[i].GetComponent<Achieve>().TargetNumber)
            {
                if (!allAchieves[i].GetComponent<Achieve>().done)
                {
                    Debug.Log("ААААЧЧЧЧИИВВВВКККАААА");
                }
            }
        }
    }
}