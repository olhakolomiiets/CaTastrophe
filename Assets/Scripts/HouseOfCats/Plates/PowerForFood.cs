using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PowerForFood : MonoBehaviour, IPlateInterface
{
    public static PowerForFood instance;
    [SerializeField] private List<FloatSO> catPowersSO;
    public string timeWhenFoodAddPref;
    public string exitTimeFoodPref;
    public string secondsLeftFoodPref;
    public string PlateUpgradePrefFloor;
    public float msFoodTime;
    public float foodTimer;
    public float secondsWhenWereExit;
    public int secAfterExit;
    public float secForFoodAll;
    public float pointsWhenWereExit;
    private bool toggle;
    public float secondsLeft;
    public int iconsFood;
    public delegate void Plate1FoodAddDelegate();

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
        TimerForFood.PlateAddFood += AddFood;
    }

    void Update()
    {
        if (foodTimer >= msFoodTime)
        {
            if (iconsFood == 0)
            {
                PassivePowerUp.FoodSpeeUp = false;
                toggle = true;
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
            PassivePowerUp.FoodSpeeUp = true;
            toggle = false;
        }
    }

    public void AddFood()
    {
        foodTimer = 0;
        secForFoodAll = 0;
        PlayerPrefs.SetString(timeWhenFoodAddPref, System.DateTime.Now.ToBinary().ToString());
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
            var foodTimerAndIcons = secondsLeft + (msFoodTime * iconsFood);
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
        foodTimer = msFoodTime - secondsLeft;

        secondsWhenWereExit = totalSecondsFoodLeft - secondsLeft;

        secForFoodAll = (float)foodTimer;
        TimeSpan timer = TimeSpan.FromSeconds(rawTime);

        if (secAfterExit < secondsWhenWereExit)
        {
            pointsWhenWereExit = secAfterExit * 0.00083333f;
            if (pointsWhenWereExit > 0)
            {
                foreach (FloatSO catPower in catPowersSO)
                {
                    if (catPower.Value < 10)
                    {
                        PassivePowerUp.foodPointsWhenWereExit = pointsWhenWereExit;
                    }
                }
            }
        }
        if (secAfterExit > secondsWhenWereExit)
        {
            if (secondsWhenWereExit > 0)
            {
                float pointsForActiveFood = secondsWhenWereExit * 0.00083333f;
                pointsWhenWereExit = pointsForActiveFood;

                if (pointsWhenWereExit > 0)
                {
                    foreach (FloatSO catPower in catPowersSO)
                    {
                        if (catPower.Value < 10)
                        {
                            PassivePowerUp.foodPointsWhenWereExit = pointsWhenWereExit;
                        }
                    }
                }
            }
            else if ((int)secondsWhenWereExit == 0)
            {
                PassivePowerUp.foodPointsWhenWereExit = 0;
            }
        }
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
        TimerForFood.PlateAddFood -= AddFood;
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
