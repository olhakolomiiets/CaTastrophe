using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardStreetManager : MonoBehaviour
{
    public static AwardStreetManager instance = null;
    public List<Transform> allAchieves = new List<Transform>();
    private GameObject _Achieves;

    void Awake()
    {
        _Achieves = gameObject.transform.GetChild(0).gameObject;
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        foreach (Transform child in _Achieves.transform)
        {
            allAchieves.Add(child);
        }
        // foreach (var x in allAchieves)
        // {
        //     // Debug.Log(x.ToString());
        // }
    }
    void Update()
    {
        StartCoroutine(CheckAhiveCorr());
    }

    IEnumerator CheckAhiveCorr()
    {
        for (int i = 0; i < allAchieves.Count; i++)

        {
            if (allAchieves[i].GetComponent<AchieveForManager>().realNumber >= allAchieves[i].GetComponent<AchieveForManager>().TargetNumber)
            {
                if (PlayerPrefs.GetInt("AchievementStateStreet_" + i) != 1)
                {
                    PlayerPrefs.SetInt("AchievementStateStreet_" + i, 1);
                    allAchieves[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    allAchieves[i].gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    yield return new WaitForSeconds(3f);
                    allAchieves[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    allAchieves[i].gameObject.transform.GetChild(1).gameObject.SetActive(false);

                    Debug.Log("ААААЧЧЧЧИИВВВВКККАААА");
                }
            }
        }
    }
}