using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Award : MonoBehaviour
{
    [SerializeField] private string AchieveNamePref;
    public int TargetNumber;
    public int TargetNumber2;
    public int TargetNumber3;
    public int realNumber;
    [SerializeField] private GameObject awardBronze;
    [SerializeField] private GameObject awardSilver;
    [SerializeField] private GameObject awardGold;

    void Start()
    {

    }
    private void OnEnable()
    {
        realNumber = GetAchieveScore();
        if (realNumber >= TargetNumber)
        {
            awardBronze.SetActive(true);
        }
        if (realNumber >= TargetNumber2)
        {
            awardSilver.SetActive(true);
        }
        if (realNumber >= TargetNumber3)
        {
            awardGold.SetActive(true);
        }
    }
    public int GetAchieveScore()
    {
        if (PlayerPrefs.HasKey(AchieveNamePref))
        {
            return PlayerPrefs.GetInt(AchieveNamePref);
        }
        else
        {
            return 0;
        }
    }
}