using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyCat : MonoBehaviour, IClickable
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

    [SerializeField] private Text textTotalRestore;

    void Start()
    {
        if (gameObject.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            PassivePowerUp.instance.ValueChangeBy.AddListener(method);  //check for othet type of event
        }
        Load();
        if (transform.GetChild(0).gameObject.activeInHierarchy)
        {            
            totalEnergy = totalEnergy + powerPoints.pointsWhenYouWereAbsent / 2;
            CheckBelowZero();
            CheckMaxEnergy();

            Invoke("CheckFoodAndToilet", 0.1f);
        }
    }

    public void method(float powerSO)
    {
        UpdateEnergy();
    }
    public void UseEnergy()
    {
        if (totalEnergy == 0f)
        {
            return;
        }
        UpdateEnergy();
        powerSO.ChangeAmountBy(-2f);
        CheckBelowZero();
        PlayerPrefs.SetFloat(totalEnergyCatPref, totalEnergy);
        UpdateEnergy();
    }
    private IEnumerator RestoreRoutine()
    {
        UpdateTimer();
        restoring = true;
        while (powerSO.Value < maxEnergy)
        {
            DateTime currentTime = DateTime.Now;
            DateTime counter = nextEnergyTime;
            bool isAdding = false;
            while (currentTime > counter)
            {
                if (powerSO.Value < maxEnergy)
                {
                    if (foodSpeeUp == false)
                    {
                        isAdding = true;
                        DateTime timeToAdd = lastAddedTime > counter ? lastAddedTime : counter;
                        counter = AddDuration(timeToAdd, restoreDuration);
                    }
                    if (foodSpeeUp == true && toiletSpeeUp == false)
                    {
                        isAdding = true;
                        totalEnergy = totalEnergy + 0.01666667f * 2;
                        DateTime timeToAdd = lastAddedTime > counter ? lastAddedTime : counter;
                        counter = AddDuration(timeToAdd, restoreDuration);
                    }
                    if (foodSpeeUp == true && toiletSpeeUp == true)
                    {
                        isAdding = true;
                        totalEnergy = totalEnergy + 0.01666667f * 3;
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
        slider.value = (int)powerSO.Value;
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
        nextEnergyTime = StringToDate(PlayerPrefs.GetString(nextEnergyTimeCatPref));
        lastAddedTime = StringToDate(PlayerPrefs.GetString(lastAddedTimeCatPref));

    }
    public void Save()
    {
        PlayerPrefs.SetFloat(totalEnergyCatPref, totalEnergy);
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

    public void CheckMaxEnergy()
    {
        if (powerSO.Value >= maxEnergy)
        {
            powerSO.SetNewAmount(maxEnergy);
            slider.value = maxEnergy;
        }
    }

    public void EnergyRestore()
    {
        powerSO.SetNewAmount(maxEnergy);
        slider.value = maxEnergy;
        value.text = maxEnergy.ToString();
    }

    private void CheckFoodAndToilet()
    {
        int trueCount = 0;
        int toiletCount = 0;
        int foodCount = 0;

        if (PassivePowerUp.FoodSpeeUp)
        {
            trueCount++;
            foodCount++;
        }

        if (PassivePowerUp.Food2SpeeUp)
        {
            trueCount++;
            foodCount++;
        }

        if (PassivePowerUp.Food3SpeeUp)
        {
            trueCount++;
            foodCount++;
        }

        if (PassivePowerUp.ToiletSpeeUp)
        {
            trueCount++;
            toiletCount++;
        }

        if (PassivePowerUp.Toilet2SpeeUp)
        {
            trueCount++;
            toiletCount++;
        }

        if (PassivePowerUp.Toilet3SpeeUp)
        {
            trueCount++;
            toiletCount++;
        }

        switch (trueCount)
        {
            case 1:
                textTotalRestore.text = $"{"+1.5"} {Lean.Localization.LeanLocalization.GetTranslationText("Per20min")}";
                break;

            case 2:
                textTotalRestore.text = $"{"+2"} {Lean.Localization.LeanLocalization.GetTranslationText("Per20min")}";
                break;

            case 3:
                textTotalRestore.text = $"{"+2.5"} {Lean.Localization.LeanLocalization.GetTranslationText("Per20min")}";
                break;

            case 4:
                textTotalRestore.text = $"{"+3"} {Lean.Localization.LeanLocalization.GetTranslationText("Per20min")}";
                break;

            case 5:
                textTotalRestore.text = $"{"+3.5"} {Lean.Localization.LeanLocalization.GetTranslationText("Per20min")}";
                break;

            case 6:
                textTotalRestore.text = $"{"+4"} {Lean.Localization.LeanLocalization.GetTranslationText("Per20min")}";
                break;

            default:
                textTotalRestore.text = $"{"+1"} {Lean.Localization.LeanLocalization.GetTranslationText("Per20min")}";
                break;

        }
    }
}
