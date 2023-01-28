using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PowerForToilet : MonoBehaviour, IToiletInterface
{
    public static PowerForToilet instance;
    [SerializeField] private List<FloatSO> catPowersSO;
    public string timeWhenToiletCleanedPref;
    public string exitTimeToiletPref;
    public string secondsLeftToiletPref;
    public string ToiletUpgradePrefFloor;
    public float msToiletTime = 60f;
    public float toiletTimer = 0;
    public float secondsLeft;
    public float toiletTimerLeft;
    public float secondsWhenWereExit;
    public int secAfterExit;
    public float secForToiletAll;
    public float pointsWhenWereExit;
    private bool toggle;
    private bool saved = true;
    public delegate void Toilet1CleanUpDelegate();

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
        UpdateMsToiletTime();

        if (saved)
        {
            Load();
        }
        Debug.Log("!!!!!!!!!!!!!!!!!!!!! Awake POwerToilet");
    }

    public void UpdateMsToiletTime()
    {
        var purch = PlayerPrefs.GetInt(ToiletUpgradePrefFloor, 0);
        if (purch == 0)
        {
            msToiletTime = 60f;
        }
        if (purch == 1)
        {
            msToiletTime = 120f;
        }
        if (purch == 2)
        {
            msToiletTime = 180f;
        }
    }
    void Start()
    {
        StartCoroutine(DebugLogToiletSec());
        TimerForToilet.ToiletAddSand += AddSand;
        Debug.Log("SUB");
        Debug.Log("!!!!!!!!!!!!!!!!!!!!! Start POwerToilet");
    }
    private void OnEnable()
    {
        Debug.Log("!!!!!!!!!!!!!!!!!!!!! OnEnable POwerToilet");
    }

    void Update()
    {
        if (toiletTimer > msToiletTime)
        {
            PassivePowerUp.ToiletSpeeUp = false;
            toggle = true;
        }
        else if (toiletTimer <= msToiletTime)
        {
            secForToiletAll += (1 * Time.deltaTime);
            toiletTimer = secForToiletAll;
            float secondsLeft = (msToiletTime - toiletTimer);
            PassivePowerUp.ToiletSpeeUp = true;
            toggle = false;
        }
    }

    public void AddSand()
    {
        toiletTimer = 0;
        secForToiletAll = 0;
        PlayerPrefs.SetString(timeWhenToiletCleanedPref, System.DateTime.Now.ToBinary().ToString());
        Debug.Log(" Toilet EVENT AddSand in PowerForToilet ----------------------------------- Sand ADDED");
        PlayerPrefs.SetString("timeWhenRepairObjectOrToiletCleanedPref1", System.DateTime.Now.ToBinary().ToString());
    }

    public void Save()
    {
        PlayerPrefs.SetString(exitTimeToiletPref, System.DateTime.Now.ToBinary().ToString());
        Debug.Log(" Before Save toiletTimer1 " + toiletTimer + "  And msToiletTime is  -  " + msToiletTime);
        if (toiletTimer > msToiletTime)
        {
            PlayerPrefs.SetFloat(secondsLeftToiletPref, msToiletTime);
            Debug.Log(" Save to secondsLeftToiletPref in Toilet1 " + msToiletTime);
        }
        else if (toiletTimer < msToiletTime)
        {
            PlayerPrefs.SetFloat(secondsLeftToiletPref, toiletTimer);
            Debug.Log(" Save to secondsLeftToiletPref in Toilet1 " + toiletTimer);
        }
        saved = true;
    }

    public void Load()
    {
        Debug.Log("!________________________________________________________ LOOOOOOOAAAAAAADDDDDDD " + this.gameObject.name);
        long tempExitTimeToilet = 0;
        if (PlayerPrefs.HasKey(exitTimeToiletPref))
        {
            tempExitTimeToilet = Convert.ToInt64(PlayerPrefs.GetString(exitTimeToiletPref));
        }
        var exitTimeToilet = DateTime.FromBinary(tempExitTimeToilet);
        var currentTime = DateTime.Now;
        var difference = currentTime.Subtract(exitTimeToilet);
        var rawTime = (float)difference.TotalSeconds;
        secAfterExit = (int)rawTime;

        if (!PlayerPrefs.HasKey("firstInitPowerForToilet1"))
        {
            toiletTimer = 0;
            secondsLeft = msToiletTime;
            PlayerPrefs.SetInt("firstInitPowerForToilet1", 1);
            PlayerPrefs.SetFloat(secondsLeftToiletPref, toiletTimer);
            // Debug.Log("!________________________________________________________ PlayerPrefs FirstInitPowerForToilet1 SecondsLeft " + secondsLeft);
            PlayerPrefs.SetString(timeWhenToiletCleanedPref, System.DateTime.Now.ToBinary().ToString());
            PlayerPrefs.SetString("timeWhenRepairObjectOrToiletCleanedPref1", System.DateTime.Now.ToBinary().ToString());
        }
        else
        {
            toiletTimer = PlayerPrefs.GetFloat(secondsLeftToiletPref);
            Debug.Log("!________________________________________________________ Load to ToiletTimer " + toiletTimer);
            secondsLeft = msToiletTime - (toiletTimer + secAfterExit);
            if (secondsLeft < 0)
            {
                secondsLeft = 0;
            }
            Debug.Log("!________________________________________________________ Load to SecondsLeft " + secondsLeft);
            var toiletSecondsWhenWereExit = msToiletTime - toiletTimer - secondsLeft;
            Debug.Log("!________________________________________________________ Load to secondsToiletLeft " + toiletSecondsWhenWereExit);
            secondsWhenWereExit = (int)toiletSecondsWhenWereExit;
        }

        toiletTimer = msToiletTime - secondsLeft;
        Debug.Log("!________________________________________________________ Load to ToiletTimer " + toiletTimer);

        secForToiletAll = (float)toiletTimer;
        TimeSpan timer = TimeSpan.FromSeconds(rawTime);


        if (secAfterExit <= secondsWhenWereExit)
        {
            Debug.Log(" if secAfterExit " + secAfterExit + " < secondsToiletLeft " + secondsWhenWereExit);
            pointsWhenWereExit = secAfterExit * 0.00083333f;
            Debug.Log("pointsWhenWereExit " + pointsWhenWereExit);
            if (pointsWhenWereExit > 0)
            {
                foreach (FloatSO catPower in catPowersSO)
                {
                    if (catPower.Value < 10)
                    {
                        PassivePowerUp.toiletPointsWhenWereExit = pointsWhenWereExit;
                        Debug.Log("Toilet Points when you was absent Timer NotFinished " + pointsWhenWereExit + " to Cat " + catPower.name);
                    }
                }
            }
        }
        if (secAfterExit > secondsWhenWereExit)
        {
            Debug.Log(" Toilet if secAfterExit " + secAfterExit + " > secondsToiletLeft " + secondsWhenWereExit);
            if (secondsWhenWereExit > 0)
            {
                float pointsForActiveToilet = secondsWhenWereExit * 0.00083333f;
                pointsWhenWereExit = pointsForActiveToilet;

                if (pointsWhenWereExit > 0)
                {
                    foreach (FloatSO catPower in catPowersSO)
                    {
                        if (catPower.Value < 10)
                        {
                            PassivePowerUp.toiletPointsWhenWereExit = pointsWhenWereExit;
                            Debug.Log("Toilet Points when you was absent Timer Finished " + pointsWhenWereExit + " to Cat " + catPower.name + " Where exit time "
                            + secAfterExit + " and Toilet Timer left - " + secondsWhenWereExit);
                        }
                    }
                }
            }
            else if ((int)secondsWhenWereExit == 0)
            {
                PassivePowerUp.toiletPointsWhenWereExit = 0;
            }
        }
        saved = false;
    }
    private void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus)
        {
            if (saved)
            {
                Load();
                Debug.Log("Load OnOnFocus POwerToilet !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }
        // else
        // {
        //     Save();
        //     Debug.Log("Save OnOnFocus POwerToilet !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        // }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Save();
            Debug.Log("Save OnOnPause POwerToilet !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        else
        {
            if (saved)
            {
                Load();
                Debug.Log("Load OnOnPause POwerToilet !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }

    }

    private void OnDisable()
    {
        TimerForToilet.ToiletAddSand -= AddSand;
        StopCoroutine(DebugLogToiletSec());
    }

    private void OnDestroy()
    {
        Save();
        Debug.Log("Save OnDestroy POwerToilet !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }

    public float ToiletTimer()
    {
        return toiletTimer;
    }

    private IEnumerator DebugLogToiletSec()
    {
        while (true)
        {
            if (!toggle)
            {
                Debug.Log("toiletTimer         " + toiletTimer);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}