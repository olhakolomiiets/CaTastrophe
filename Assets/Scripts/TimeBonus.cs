using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBonus : MonoBehaviour
{
    [SerializeField] private GameTimer timer;
    [SerializeField] private ScoreManager sm;
    [SerializeField]
    [Header("Bonus 1")]
    public string bonusIdPref1;
    public int HowMany1;
    public int secAdd1;
    [SerializeField] private GameObject done1;
    private bool completed1;
    public Text textBonus1;
    [Header("Bonus 2")]
    public string bonusIdPref2;
    public int HowMany2;
    public int secAdd2;
    [SerializeField] private GameObject done2;
    private bool completed2;
    public Text textBonus2;
    [Header("Bonus 3")]
    public string bonusIdPref3;
    public int HowMany3;
    public int secAdd3;
    [SerializeField] private GameObject done3;
    private bool completed3;
    public Text textBonus3;
    private bool[] completed;
    private GameObject[] done;
    private string[] bonusIdPref;
    private int[] HowMany;
    private int[] secAdd;
    private Text[] textBonus;
    public delegate void TimeBonusDelegate();
    private void OnEnable()
    {
        done = new GameObject[] { done1, done2, done3 };
        bonusIdPref = new string[] { bonusIdPref1, bonusIdPref2, bonusIdPref3 };
        HowMany = new int[] { HowMany1, HowMany2, HowMany3 };
        secAdd = new int[] { secAdd1, secAdd2, secAdd3 };
        textBonus = new Text[] { textBonus1, textBonus2, textBonus3 };
        completed = new bool[] { completed1, completed2, completed3 };

        for (int i = 0; i < bonusIdPref.Length; i++)
        {
            textBonus[i].text = $"{PlayerPrefs.GetInt(bonusIdPref[i])}/{HowMany[i]}    +{secAdd[i]} sec";
        }

        sm.UpdateBonusScore += CheckForTimeBonuses;
        CheckForTimeBonuses();
    }
    private void CheckForTimeBonuses()
    {
        for (int i = 0; i < bonusIdPref.Length; i++)
        {
            if (PlayerPrefs.GetInt(bonusIdPref[i]) == HowMany[i] && !completed[i])
            {
                Debug.Log("!!!______________________________________________________ CheckForTimeBonuses " + bonusIdPref[i]);
                completed[i] = true;
                done[i].SetActive(true);
                timer.AddSecondsToTimer(secAdd[i]);
            }
            textBonus[i].text = $"{PlayerPrefs.GetInt(bonusIdPref[i])}/{HowMany[i]}    +{secAdd[i]} sec";
        }
    }

    // private void CheckForTimeBonuses()
    // {
    //     if (PlayerPrefs.GetInt(bonusIdPref1) == HowMany1 && !completed1)
    //     {
    //         textBonus1.text = $"{PlayerPrefs.GetInt(bonusIdPref1)}/{HowMany1}    +{secAdd1} sec";
    //         completed1 = true;
    //         done1.SetActive(true);
    //         timer.AddSecondsToTimer(secAdd1);
    //     }
    //     if (PlayerPrefs.GetInt(bonusIdPref2) == HowMany2 && !completed2)
    //     {
    //         textBonus2.text = $"{PlayerPrefs.GetInt(bonusIdPref2)}/{HowMany2}    +{secAdd2} sec";
    //         completed2 = true;
    //         done2.SetActive(true);
    //         timer.AddSecondsToTimer(secAdd2);
    //     }
    //     if (PlayerPrefs.GetInt(bonusIdPref3) == HowMany3 && !completed3)
    //     {
    //         textBonus3.text = $"{PlayerPrefs.GetInt(bonusIdPref3)}/{HowMany3}    +{secAdd3} sec";
    //         completed3 = true;
    //         done3.SetActive(true);
    //         timer.AddSecondsToTimer(secAdd3);
    //     }
    //     textBonus1.text = $"{PlayerPrefs.GetInt(bonusIdPref1)}/{HowMany1}    +{secAdd1} sec";
    //     textBonus2.text = $"{PlayerPrefs.GetInt(bonusIdPref2)}/{HowMany2}    +{secAdd2} sec";
    //     textBonus3.text = $"{PlayerPrefs.GetInt(bonusIdPref3)}/{HowMany3}    +{secAdd3} sec";
    // }

    private void OnDestroy()
    {
        sm.UpdateBonusScore -= CheckForTimeBonuses;
        for (int i = 0; i < bonusIdPref.Length; i++)
        {
            PlayerPrefs.SetInt(bonusIdPref[i], 0);
        }
    }
}
