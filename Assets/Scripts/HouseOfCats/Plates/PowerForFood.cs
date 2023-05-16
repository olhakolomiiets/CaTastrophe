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
    // public float secondsFoodLeft;
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
        //StartCoroutine(DebugLogPlateSec());
        TimerForFood.PlateAddFood += AddFood;
        //Debug.Log("SUB");
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
        // Debug.Log("Plate1 EVENT AddFood in PowerForFood ----------------------------------- Food ADDED");
    }

    public void Save()
    {
        PlayerPrefs.SetString(exitTimeFoodPref, System.DateTime.Now.ToBinary().ToString());
        // Debug.Log(" Before Save foodTimer1 " + foodTimer + "  And msFoodTime is  -  " + msFoodTime);
        if (foodTimer >= msFoodTime)
        {
            PlayerPrefs.SetFloat(secondsLeftFoodPref, secondsLeft);
            // Debug.Log(" Save to secondsLeftFoodPref in Plate1 " + secondsLeft);
        }
        else if (foodTimer < msFoodTime)
        {
            var foodTimerAndIcons = secondsLeft + (msFoodTime * iconsFood);
            PlayerPrefs.SetFloat(secondsLeftFoodPref, foodTimerAndIcons);
            // PlayerPrefs.SetFloat(secondsLeftFoodPref, foodTimer);
            // Debug.Log(" Save to secondsLeftFoodPref in Plate1 " + foodTimerAndIcons);
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
        // Debug.Log("!________________________________________________________ Load to totalSecondsFoodLeft " + totalSecondsFoodLeft);
        if (totalSecondsFoodLeft > secAfterExit)
        {
            var SecFoodWithoutSecExit = (int)totalSecondsFoodLeft - secAfterExit;
            if (SecFoodWithoutSecExit > msFoodTime)
            {
                iconsFood = SecFoodWithoutSecExit / ((int)msFoodTime);
            }
            else iconsFood = 0;
            // Debug.Log("!________________________________________________________  Load to IconsFood " + iconsFood);
            secondsLeft = (SecFoodWithoutSecExit - (msFoodTime * iconsFood));
        }
        else
        {
            iconsFood = 0;
            secondsLeft = 0;
        }
        // Debug.Log("!________________________________________________________ Load to SecondsLeft " + secondsLeft);
        foodTimer = msFoodTime - secondsLeft;
        // Debug.Log("!________________________________________________________ Load to FoodTimer " + foodTimer);

        secondsWhenWereExit = totalSecondsFoodLeft - secondsLeft;

        // secondsFoodLeft = secondsLeft + (msFoodTime * iconsFood);
        // Debug.Log("!________________________________________________________ Load to SecondsFoodLeft " + secondsFoodLeft);
        secForFoodAll = (float)foodTimer;
        TimeSpan timer = TimeSpan.FromSeconds(rawTime);

        if (secAfterExit < secondsWhenWereExit)
        {
            //Debug.Log(" if secAfterExit " + secAfterExit + " < secondsWhenWereExit " + secondsWhenWereExit);
            pointsWhenWereExit = secAfterExit * 0.00083333f;
            //Debug.Log("pointsWhenWereExit " + pointsWhenWereExit);
            if (pointsWhenWereExit > 0)
            {
                foreach (FloatSO catPower in catPowersSO)
                {
                    if (catPower.Value < 10)
                    {
                        PassivePowerUp.foodPointsWhenWereExit = pointsWhenWereExit;
                        //Debug.Log("Plate1 Points when you was absent Timer NotFinished " + pointsWhenWereExit + " to Cat " + catPower.name);
                    }
                }
            }
        }
        if (secAfterExit > secondsWhenWereExit)
        {
            //Debug.Log(" Plate1 if secAfterExit " + secAfterExit + " > secondsWhenWereExit " + secondsWhenWereExit);
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
                            //Debug.Log("Plate1 Points when you was absent Timer Finished " + pointsWhenWereExit + " to Cat " + catPower.name + " Where exit time " + secAfterExit + " and Plate Timer left - " + secondsWhenWereExit);
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
            //Debug.Log("Load OnOnFocus POwerFood !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        else
        {
            Save();
            //Debug.Log("Save OnOnFocus POwerFood !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Save();
            //Debug.Log("Save OnOnPause POwerFood !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        else
        {
            Load();
            //Debug.Log("Load OnOnPause POwerFood !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }

    private void OnDisable()
    {
        TimerForFood.PlateAddFood -= AddFood;
        //StopCoroutine(DebugLogPlateSec());
    }

    private void OnDestroy()
    {
        Save();
        //Debug.Log("Save OnDestroy POwerFood !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
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

/*    private IEnumerator DebugLogPlateSec()
    {
        while (true)
        {
            if (!toggle)
            {
                Debug.Log("foodTimer         " + secondsLeft);
            }
            yield return new WaitForSeconds(1f);
        }
    }*/

}
