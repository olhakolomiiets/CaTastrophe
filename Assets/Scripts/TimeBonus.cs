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

    private void Awake()
    {
        done = new GameObject[] { done1, done2, done3 };
        bonusIdPref = new string[] { bonusIdPref1, bonusIdPref2, bonusIdPref3 };
        HowMany = new int[] { HowMany1, HowMany2, HowMany3 };
        secAdd = new int[] { secAdd1, secAdd2, secAdd3 };
        textBonus = new Text[] { textBonus1, textBonus2, textBonus3 };
        completed = new bool[] { completed1, completed2, completed3 };

        sm.UpdateBonusScore += CheckForTimeBonuses;

        for (int i = 0; i < bonusIdPref.Length; i++)
        {
            textBonus[i].text = $"{PlayerPrefs.GetInt(bonusIdPref[i])}/{HowMany[i]}    +{secAdd[i]} {Lean.Localization.LeanLocalization.GetTranslationText("Seconds")}";
        }       
    }

    private void Start()
    {       
        CheckForTimeBonuses();
    }
    private void CheckForTimeBonuses()
    {
        for (int i = 0; i < bonusIdPref.Length; i++)
        {
            if (PlayerPrefs.GetInt(bonusIdPref[i]) == HowMany[i] && completed[i] == false)
            {
                completed[i] = true;
                done[i].SetActive(true);
                timer.AddSecondsToTimer(secAdd[i]);                
            }
            textBonus[i].text = $"{PlayerPrefs.GetInt(bonusIdPref[i])}/{HowMany[i]}    +{secAdd[i]} {Lean.Localization.LeanLocalization.GetTranslationText("Seconds")}";
        }
    }

    private void OnDestroy()
    {
        sm.UpdateBonusScore -= CheckForTimeBonuses;
        for (int i = 0; i < bonusIdPref.Length; i++)
        {
            PlayerPrefs.SetInt(bonusIdPref[i], 0);
        }
    }
}
