using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{

    [SerializeField] private GameObject intro;

    void Start()
    {
        if (PlayerPrefs.GetInt("Intro") == 0)
        {
            intro.SetActive(true);
            PauseOn();
            PlayerPrefs.SetInt("Intro", 1);
            InitializationPrefs();
        }
    }

    public void PauseOn()
    {
        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        Time.timeScale = 1;
    }

    private void InitializationPrefs()
    {
        PlayerPrefs.SetInt("AwardVaseDestroyed", 0);
        PlayerPrefs.SetInt("AwardPoopInShoes", 0);
        PlayerPrefs.SetInt("AwardTotalMoney", 0);
        PlayerPrefs.SetInt("AwardMoneyPerHouse", 0);
        PlayerPrefs.SetInt("AwardPaintUsed", 0);
        PlayerPrefs.SetInt("AwardHeavyObj", 0);
        PlayerPrefs.SetInt("CatsIsYoursAchieve", 0);
    }
}
