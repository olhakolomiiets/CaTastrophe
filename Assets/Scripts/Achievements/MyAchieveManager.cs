using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAchieveManager : MonoBehaviour
{
    public static MyAchieveManager instance = null;
    private List<Transform> allAchieves = new List<Transform>();
    private List<Transform> housesAchieves = new List<Transform>();
    private GameObject _Achieves;
    private GameObject _AchievesHouses;

    void Awake()
    {

        _Achieves = gameObject.transform.GetChild(0).gameObject;
        _AchievesHouses = gameObject.transform.GetChild(2).gameObject;

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
        foreach (var x in allAchieves)
        {
            // Debug.Log(x.ToString());
        }
        foreach (Transform child in _AchievesHouses.transform)
        {
            housesAchieves.Add(child);
        }
        foreach (var x in housesAchieves)
        {
            // Debug.Log(x.ToString());
        }
    }
    void Update()
    {
        StartCoroutine(CheckAhiveCorr());
        StartCoroutine(CheckAchiveHouses());
    }
    IEnumerator CheckAchiveHouses()
    {
        for (int i = 0; i < housesAchieves.Count; i++)
        {
            if (housesAchieves[i].GetComponent<AchieveForManager>().realNumber >= housesAchieves[i].GetComponent<AchieveForManager>().TargetNumber
            && housesAchieves[i].GetComponent<AchieveForManager>().allStars >= housesAchieves[i].GetComponent<AchieveForManager>().StarsNeeds)
            {
                if (PlayerPrefs.GetInt("AchievementHouseState_" + i) == 0)
                {
                    PlayerPrefs.SetInt("AchievementHouseState_" + i, 1);

                    housesAchieves[i].gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    housesAchieves[i].gameObject.transform.GetChild(1).gameObject.SetActive(true);
                    yield return new WaitForSeconds(3f);
                    housesAchieves[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    housesAchieves[i].gameObject.transform.GetChild(1).gameObject.SetActive(false);

                    Debug.Log("ААААЧЧЧЧИИВВВВКККАААА");
                }
            }
        }
    }
    IEnumerator CheckAhiveCorr()
    {
        for (int i = 0; i < allAchieves.Count; i++)

        {
            if (allAchieves[i].GetComponent<AchieveForManager>().realNumber >= allAchieves[i].GetComponent<AchieveForManager>().TargetNumber)
            {

                if (PlayerPrefs.GetInt("AchievementState_" + i) == 0)
                {
                    PlayerPrefs.SetInt("AchievementState_" + i, 1);
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
    public void ResetAchieves()
    {
        PlayerPrefs.SetInt("VaseAchieve", 0);
        PlayerPrefs.SetInt("ShitBootAchieve", 0);


        PlayerPrefs.SetInt("NotebookPeeTipUsed", 0);
        PlayerPrefs.SetInt("shoesShitTipUsed", 0);
        PlayerPrefs.SetInt("chairDestroyTipUsed", 0);
        PlayerPrefs.SetInt("pillowTipUsed", 0);
        PlayerPrefs.SetInt("courtainDestroyTipUsed", 0);
        PlayerPrefs.SetInt("TuleTipUsed", 0);
        PlayerPrefs.SetInt("LampTip", 0);
        PlayerPrefs.SetInt("bedShitTip", 0);
        PlayerPrefs.SetInt("sofaShitTipUsed", 0);


        PlayerPrefs.SetInt("TvIsBought", 0);
        PlayerPrefs.SetInt("LampIsBought", 0);
        PlayerPrefs.SetInt("bedShit", 0);
        PlayerPrefs.SetInt("sofaShit", 0);
        PlayerPrefs.SetInt("pillow", 0);
        for (int i = 0; i < allAchieves.Count; i++)
        {
            PlayerPrefs.SetInt("AchievementState_" + i, 0);
        }
    }
}