using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAchieveManager : MonoBehaviour
{
    public static MyAchieveManager instance = null;
    private List<Transform> allAchieves = new List<Transform>();

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