using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardStreetManager : MonoBehaviour
{
    public static AwardStreetManager instance = null;

    [SerializeField] private List<AchieveSO> allAchievesSO = new List<AchieveSO>();
    [SerializeField] private GameObject _allAwardsGrid;
    private IEnumerator coroutine;

    void Awake()
    {

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


    IEnumerator CheckAhiveCorr1()
    {
        while (true) // loop infinitely
        {
            for (int i = 0; i < allAchievesSO.Count; i++)

            {
                if (allAchievesSO[i].GetAchieveScore() >= allAchievesSO[i].TargetNumberLvl1)
                {

                    if (PlayerPrefs.GetInt("AchievementState_1" + i) == 0)
                    {
                        PlayerPrefs.SetInt("AchievementState_1" + i, 1);

                        GameObject newPrefabInstance = Instantiate(allAchievesSO[i].AchieveLvl1, _allAwardsGrid.transform);
                        yield return new WaitForSeconds(3f);
                        Destroy(newPrefabInstance);

                        Debug.Log("ААААЧЧЧЧИИВВВВКККАААА");
                    }
                }
                if (allAchievesSO[i].GetAchieveScore() >= allAchievesSO[i].TargetNumberLvl2)
                {

                    if (PlayerPrefs.GetInt("AchievementState_2" + i) == 0)
                    {
                        PlayerPrefs.SetInt("AchievementState_2" + i, 1);

                        GameObject newPrefabInstance = Instantiate(allAchievesSO[i].AchieveLvl2, _allAwardsGrid.transform);
                        yield return new WaitForSeconds(3f);
                        Destroy(newPrefabInstance);

                        Debug.Log("ААААЧЧЧЧИИВВВВКККАААА");
                    }
                }
                if (allAchievesSO[i].GetAchieveScore() >= allAchievesSO[i].TargetNumberLvl3)
                {

                    if (PlayerPrefs.GetInt("AchievementState_3" + i) == 0)
                    {
                        PlayerPrefs.SetInt("AchievementState_3" + i, 1);

                        GameObject newPrefabInstance = Instantiate(allAchievesSO[i].AchieveLvl3, _allAwardsGrid.transform);
                        yield return new WaitForSeconds(3f);
                        Destroy(newPrefabInstance);

                        Debug.Log("ААААЧЧЧЧИИВВВВКККАААА");
                    }
                }
            }
            yield return new WaitForSeconds(1);
            Debug.Log("ААААЧЧЧЧИИВВВВКККАААА under yield return");
        }
    }



    void OnEnable()
    {
        coroutine = CheckAhiveCorr1();
        StartCoroutine(coroutine);
    }

    void OnDisable()
    {
        StopCoroutine(coroutine);
    }
}