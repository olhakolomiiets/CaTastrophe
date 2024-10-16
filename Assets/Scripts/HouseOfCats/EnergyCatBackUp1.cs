﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyCatBackUp1 : MonoBehaviour, IClickable
{
    [SerializeField]
    private Slider slider;   
    [SerializeField]
    private Text value;
    [SerializeField]
    private float maxEnergy;
    [SerializeField]
    private string totalEnergyCatPref;
    [SerializeField]
    private string nextEnergyTimeCatPref;
    [SerializeField]
    private string lastAddedTimeCatPref;
    public float totalEnergy;
    private DateTime nextEnergyTime;
    private DateTime lastAddedTime;
    private int restoreDuration = 1;
    private bool restoring = false;
    public bool foodSpeeUp;
    public bool toiletSpeeUp;
    public PowerPointManager powerPoints;
    public FloatSO powerSO;
 
    void Start()
    {
       
        CheckBelowZero();
        Load();
        Debug.Log("+++++++++++++++++++++++++++++++ after Load totalEnergy of Cat " + totalEnergyCatPref  + " is " + totalEnergy);
        Debug.Log("++++++++++++++++++++++++++++++++ pointsWhenYouWereAbsent = " + powerPoints.pointsWhenYouWereAbsent/2);
        
        totalEnergy = totalEnergy + powerPoints.pointsWhenYouWereAbsent/2;
        Debug.Log("+++++++++++++++++++ totalEnergy of Cat after summ " + totalEnergy  + " Cat is " + totalEnergyCatPref);
        if (totalEnergy >= maxEnergy)
        {
            totalEnergy = maxEnergy;
            slider.value = totalEnergy;
            return;
        }
        CheckBelowZero();
        StartCoroutine(RestoreRoutine());
    }
    public void UseEnergy()
    {
        if (totalEnergy == 0f)
        {
            return;
        }
        UpdateEnergy();
        powerSO.ChangeAmountBy(-3f);
        // totalEnergy = totalEnergy - 3f;
       CheckBelowZero();
        PlayerPrefs.SetFloat(totalEnergyCatPref, totalEnergy);
        // Debug.Log("+++++++++++++++++++++++++++++++Use In EnergyCat Substracted from " + totalEnergyCatPref  + " - 3 = " + PlayerPrefs.GetFloat(totalEnergyCatPref));

        UpdateEnergy();
        // Debug.Log("+++++++++++++++++++++++++++++++Use In EnergyCat Substracted from " + totalEnergyCatPref  + " - 3 = " + PlayerPrefs.GetFloat(totalEnergyCatPref));

        if (restoring == false)
        {
            if (totalEnergy + 3 == maxEnergy)
            {
                nextEnergyTime = AddDuration(DateTime.Now, restoreDuration);
            }
            StartCoroutine(RestoreRoutine());
        }
    }
    private IEnumerator RestoreRoutine()
    {
        UpdateTimer();
        UpdateEnergy();
        restoring = true;
        while (totalEnergy < maxEnergy)
        {
            DateTime currentTime = DateTime.Now;
            DateTime counter = nextEnergyTime;
            bool isAdding = false;
            while (currentTime > counter)
            {
                if (totalEnergy < maxEnergy)
                {
                    if (foodSpeeUp == false)
                    {
                        isAdding = true;
                        // powerSO.ChangeAmountBy(0.01666667f);
                        totalEnergy = totalEnergy + 0.01666667f; //1 in minute
                        // Debug.Log("+++++++++++++++++++++++++++++++In EnergyCat total Energy Update " + totalEnergyCatPref  + " = " + totalEnergy);
                        DateTime timeToAdd = lastAddedTime > counter ? lastAddedTime : counter;
                        counter = AddDuration(timeToAdd, restoreDuration);
                    }
                    if (foodSpeeUp == true && toiletSpeeUp == false)
                    {
                        isAdding = true;
                        totalEnergy = totalEnergy + 0.01666667f*2;
                        DateTime timeToAdd = lastAddedTime > counter ? lastAddedTime : counter;
                        counter = AddDuration(timeToAdd, restoreDuration);
                    }
                    if (foodSpeeUp == true && toiletSpeeUp == true)
                    {
                        isAdding = true;
                        totalEnergy = totalEnergy + 0.01666667f*3;
                        DateTime timeToAdd = lastAddedTime > counter ? lastAddedTime : counter;
                        counter = AddDuration(timeToAdd, restoreDuration);
                    }                   
                }
                else
                    break;
            }
            if (isAdding)
            {
                lastAddedTime = DateTime.Now;
                nextEnergyTime = counter;
            }

            UpdateTimer();
            UpdateEnergy();
            Save();
            yield return new WaitForSeconds(1f);
        }
        restoring = false;

    }
    private void UpdateTimer()
    {
        if (totalEnergy >= maxEnergy)
        {
            return;
        }
        TimeSpan t = nextEnergyTime - DateTime.Now;
        string value = String.Format("{0}:{1:D2}:{2:D2}", (int)t.TotalHours, t.Minutes, t.Seconds);
    }
    public void UpdateEnergy()
    {
        // slider.value = (int)totalEnergy;
        // Debug.Log("+++++++++++++++++++++++++++++++ slider.value = " + (int)totalEnergy);
        // var x = (int)totalEnergy;
        // value.text = x.ToString();
        slider.value = (int)powerSO.Value;
        Debug.Log("+++++++++++++++++++++++++++++++ slider.value = " + (int)totalEnergy);
        var x = (int)powerSO.Value;
        value.text = x.ToString();
    }
    private DateTime AddDuration(DateTime time, int duration)
    {
        return time.AddSeconds(duration);
    }
    public void Load()
    {
        totalEnergy = PlayerPrefs.GetFloat(totalEnergyCatPref, 10);
        // Debug.Log("+++++++++++++++++++++++++++++++Load In EnergyCat totalEnergyCatPref   " + totalEnergyCatPref  + " = " + PlayerPrefs.GetFloat(totalEnergyCatPref));
        nextEnergyTime = StringToDate(PlayerPrefs.GetString(nextEnergyTimeCatPref));
        lastAddedTime = StringToDate(PlayerPrefs.GetString(lastAddedTimeCatPref));

    }
    public void Save()
    {
        PlayerPrefs.SetFloat(totalEnergyCatPref, totalEnergy);
        // Debug.Log("+++++++++++++++++++++++++++++++Save In EnergyCat totalEnergyCatPref   " + totalEnergyCatPref  + " = " + PlayerPrefs.GetFloat(totalEnergyCatPref));
        PlayerPrefs.SetString(nextEnergyTimeCatPref, nextEnergyTime.ToString());
        PlayerPrefs.SetString(lastAddedTimeCatPref, lastAddedTime.ToString());
    }

    private DateTime StringToDate(string date)
    {
        if (String.IsNullOrEmpty(date))
        {
            return DateTime.Now;
        }
        return DateTime.Parse(date);
    }

    public void Click()
    {
        UseEnergy();
    }
    public void CheckBelowZero()
    {
         if (totalEnergy < 0f)
        {
            totalEnergy = 0f;        
        }
    }
}
