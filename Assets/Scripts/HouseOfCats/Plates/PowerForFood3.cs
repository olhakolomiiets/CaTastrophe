using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PowerForFood3 : MonoBehaviour, IPlateInterface
{
    public static PowerForFood3 instance;
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
        StartCoroutine(DebugLogPlateSec());
        TimerForFood.Plate3AddFood += AddFood;
        Debug.Log("SUB");
    }

    void Update()
    {
        if (foodTimer >= msFoodTime)
        {
            if (iconsFood == 0)
            {
                PassivePowerUp.Food3SpeeUp = false;
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
            PassivePowerUp.Food3SpeeUp = true;
            toggle = false;
        }
    }

    public void AddFood()
    {
        foodTimer = 0;
        secForFoodAll = 0;
        PlayerPrefs.SetString(timeWhenFoodAddPref, System.DateTime.Now.ToBinary().ToString());
        Debug.Log("Plate3 EVENT AddFood in PowerForFood ----------------------------------- Food ADDED");
    }

    public void Save()
    {
        PlayerPrefs.SetString(exitTimeFoodPref, System.DateTime.Now.ToBinary().ToString());
        Debug.Log(" Before Save foodTimer3 " + foodTimer + "  And msFoodTime is  -  " + msFoodTime);
        if (foodTimer >= msFoodTime)
        {
            PlayerPrefs.SetFloat(secondsLeftFoodPref, secondsLeft);
            Debug.Log(" Save to secondsLeftFoodPref in Plate3 " + secondsLeft);
        }
        else if (foodTimer < msFoodTime)
        {
            var foodTimerAndIcons = secondsLeft + msFoodTime * iconsFood;
            PlayerPrefs.SetFloat(secondsLeftFoodPref, foodTimerAndIcons);
            // PlayerPrefs.SetFloat(secondsLeftFoodPref, foodTimer);
            Debug.Log(" Save to secondsLeftFoodPref in Plate3 " + foodTimerAndIcons);
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
        Debug.Log("!!!________________________________________________________ Load to totalSecondsFoodLeft " + totalSecondsFoodLeft);
        if (totalSecondsFoodLeft > secAfterExit)
        {
            var SecFoodWithoutSecExit = (int)totalSecondsFoodLeft - secAfterExit;
            if (SecFoodWithoutSecExit > msFoodTime)
            {
                iconsFood = SecFoodWithoutSecExit / ((int)msFoodTime);
            }
            else iconsFood = 0;
            Debug.Log("!________________________________________________________  Load to IconsFood " + iconsFood);
            secondsLeft = (SecFoodWithoutSecExit - (msFoodTime * iconsFood));
        }
        else
        {
            iconsFood = 0;
            secondsLeft = 0;
        }
        Debug.Log("!________________________________________________________ Load to SecondsLeft " + secondsLeft);
        foodTimer = msFoodTime - secondsLeft;
        Debug.Log("!________________________________________________________ Load to FoodTimer " + foodTimer);
        secondsWhenWereExit = totalSecondsFoodLeft - secondsLeft;
        secForFoodAll = (float)foodTimer;
        TimeSpan timer = TimeSpan.FromSeconds(rawTime);

        if (secAfterExit < secondsWhenWereExit)
        {
            Debug.Log(" if secAfterExit " + secAfterExit + " < secondsWhenWereExit " + secondsWhenWereExit);
            pointsWhenWereExit = secAfterExit * 0.01666666666f;
            Debug.Log("pointsWhenWereExit " + pointsWhenWereExit);
            if (pointsWhenWereExit > 0)
            {
                foreach (FloatSO catPower in catPowersSO)
                {
                    if (catPower.Value < 10)
                    {
                        PassivePowerUp.foodPoints3WhenWereExit = pointsWhenWereExit;
                        Debug.Log("Plate3 Points when you was absent Timer NotFinished " + pointsWhenWereExit + " to Cat " + catPower.name);
                    }
                }
            }
        }
        if (secAfterExit > secondsWhenWereExit)
        {
            Debug.Log(" Plate if secAfterExit " + secAfterExit + " > secondsWhenWereExit " + secondsWhenWereExit);
            if (secondsWhenWereExit > 0)
            {
                float pointsForActiveFood = secondsWhenWereExit * 0.01666666666f;
                pointsWhenWereExit = pointsForActiveFood;

                if (pointsWhenWereExit > 0)
                {
                    foreach (FloatSO catPower in catPowersSO)
                    {
                        if (catPower.Value < 10)
                        {
                            PassivePowerUp.foodPoints3WhenWereExit = pointsWhenWereExit;
                            Debug.Log("Plate3 Points when you was absent Timer Finished " + pointsWhenWereExit + " to Cat " + catPower.name + " Where exit time "
                            + secAfterExit + " and Plate Timer left - " + secondsWhenWereExit);
                        }
                    }
                }
            }
            else if ((int)secondsWhenWereExit == 0)
            {
                PassivePowerUp.foodPoints3WhenWereExit = 0;
            }
        }
    }
    private void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus)
        {
            Load();
            Debug.Log("Load OnOnFocus POwerFood !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        else
        {
            Save();
            Debug.Log("Save OnOnFocus POwerFood !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Save();
            Debug.Log("Save OnOnPause POwerFood !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        else
        {
            Load();
            Debug.Log("Load OnOnPause POwerFood !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }

    private void OnDisable()
    {
        TimerForFood.Plate3AddFood -= AddFood;
        StopCoroutine(DebugLogPlateSec());
    }

    private void OnDestroy()
    {
        Save();
        Debug.Log("Save OnDestroy POwerFood !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
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
    private IEnumerator DebugLogPlateSec()
    {
        while (true)
        {
            if (!toggle)
            {
                Debug.Log("foodTimer         " + secondsLeft);
            }
            yield return new WaitForSeconds(1f);
        }
    }

}
