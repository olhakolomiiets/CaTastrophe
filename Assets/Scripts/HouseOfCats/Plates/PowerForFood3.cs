using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PowerForFood3 : MonoBehaviour, IPlateInterface
{
    public static PowerForFood3 instance;
    public string timeWhenFoodAddPref;
    public string exitTimeFoodPref;
    public string secondsLeftFoodPref;
    public string PlateUpgradePrefFloor;
    public float msFoodTime;
    public float foodTimer;
    public float secondsWhenWereExit;
    public int secAfterExit;
    public float secForFoodAll;
    public float secondsLeft;
    public int iconsFood;
    [SerializeField] private string skillName;
    [SerializeField] private SkillManager skillManager;
    [SerializeField] private int isFoodActive;
    public delegate void Plate3FoodAddDelegate();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        Application.runInBackground = false;

        Load();
    }
    void Start()
    {
        TimerForFood.Plate3AddFood += AddFood;
    }

    void Update()
    {
        if (foodTimer >= msFoodTime)
        {
            if (iconsFood == 0 && isFoodActive == 2)
            {
                isFoodActive = 1;
                skillManager.UpdateSkill(skillName, isFoodActive);
                PlayerPrefs.SetInt("FoodPlate3State", isFoodActive);
                PassivePowerUp.Food3SpeeUp = false;
            }
            else if (iconsFood > 0)
            {
                foodTimer = 0;
                secForFoodAll = 0;
                iconsFood = iconsFood - 1;
            }
        }
        else if (foodTimer < msFoodTime)
        {
            secForFoodAll += (1 * Time.deltaTime);
            foodTimer = secForFoodAll;
            secondsLeft = (msFoodTime - foodTimer);
            PassivePowerUp.Food3SpeeUp = true;

            if (isFoodActive == 1)
            {
                isFoodActive = 2;
                skillManager.UpdateSkill(skillName, isFoodActive);
                PlayerPrefs.SetInt("FoodPlate3State", isFoodActive);
            }
        }
    }

    public void AddFood()
    {
        foodTimer = 0;
        secForFoodAll = 0;
        PlayerPrefs.SetString(timeWhenFoodAddPref, System.DateTime.Now.ToBinary().ToString());

        if (PlayerPrefs.GetInt("FoodPlate3State") == 0)
        {
            isFoodActive = 1;
            PlayerPrefs.SetInt("FoodPlate3State", isFoodActive);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString(exitTimeFoodPref, System.DateTime.Now.ToBinary().ToString());
        if (foodTimer >= msFoodTime)
        {
            PlayerPrefs.SetFloat(secondsLeftFoodPref, secondsLeft);
        }
        else if (foodTimer < msFoodTime)
        {
            var foodTimerAndIcons = secondsLeft + msFoodTime * iconsFood;
            PlayerPrefs.SetFloat(secondsLeftFoodPref, foodTimerAndIcons);
        }
    }

    public void Load()
    {
        long tempExitTimePlate = 0;
        if (PlayerPrefs.HasKey(exitTimeFoodPref))
        {
            tempExitTimePlate = Convert.ToInt64(PlayerPrefs.GetString(exitTimeFoodPref));
        }
        var exitTimePlate = DateTime.FromBinary(tempExitTimePlate);
        var currentTime = DateTime.Now;
        var difference = currentTime.Subtract(exitTimePlate);
        var rawTime = (float)difference.TotalSeconds;
        secAfterExit = (int)rawTime;

        var totalSecondsFoodLeft = PlayerPrefs.GetFloat(secondsLeftFoodPref);
        if (totalSecondsFoodLeft > secAfterExit)
        {
            var SecFoodWithoutSecExit = (int)totalSecondsFoodLeft - secAfterExit;
            if (SecFoodWithoutSecExit > msFoodTime)
            {
                iconsFood = SecFoodWithoutSecExit / ((int)msFoodTime);
            }
            else iconsFood = 0;
            secondsLeft = (SecFoodWithoutSecExit - (msFoodTime * iconsFood));
        }
        else
        {
            iconsFood = 0;
            secondsLeft = 0;
        }
        isFoodActive = PlayerPrefs.GetInt("FoodPlate3State");
        foodTimer = msFoodTime - secondsLeft;
        secondsWhenWereExit = totalSecondsFoodLeft - secondsLeft;
        secForFoodAll = (float)foodTimer;
        TimeSpan timer = TimeSpan.FromSeconds(rawTime);
    }
    private void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus)
        {
            Load();
        }
        else
        {
            Save();
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Save();
        }
        else
        {
            Load();
        }
    }

    private void OnDisable()
    {
        TimerForFood.Plate3AddFood -= AddFood;
    }

    private void OnDestroy()
    {
        Save();
    }

    public float FoodTimer()
    {
        return foodTimer;
    }

    public int IconsFood()
    {
        return iconsFood;
    }

    public float GetMsFoodTime()
    {
        return msFoodTime;
    }
}
