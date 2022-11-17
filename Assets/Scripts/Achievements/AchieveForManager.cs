using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AchieveForManager : MonoBehaviour
{
    [SerializeField]
    private string AchieveNamePref;
    public int TargetNumber;
    public int StarsNeeds;
    public int realNumber;
    public int allStars;
    public bool done;
    void Start()
    {
        if (realNumber >= TargetNumber && allStars >= StarsNeeds)
        {
            done = true;
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
    public int GetAllStars()
    {
        if (PlayerPrefs.HasKey("AllStars"))
        {
            return PlayerPrefs.GetInt("AllStars");
        }
        else
        {
            return 0;
        }
    }
    private void Update()
    {
        realNumber = GetAchieveScore();
        allStars = GetAllStars();
    }
}