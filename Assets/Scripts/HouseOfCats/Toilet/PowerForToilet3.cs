using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PowerForToilet3 : MonoBehaviour, IToiletInterface
{
    public static PowerForToilet3 instance;
    [SerializeField] private List<FloatSO> catPowersSO;
    public string timeWhenToiletCleanedPref;
    public string exitTimeToiletPref;
    public string secondsLeftToiletPref;
    public string ToiletUpgradePrefFloor;
    public float msToiletTime = 60f;
    public float toiletTimer = 0;
    public float secondsWhenWereExit;
    public float secondsLeft;
    public int secAfterExit;
    public float secForToiletAll;
    public float pointsWhenWereExit;
    private bool toggle;
    private bool saved = true;
    public delegate void Toilet3CleanUpDelegate();

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
        TimerForToilet.Toilet3AddSand += AddSand;
        Debug.Log("SUB");
    }

    void Update()
    {

        if (toiletTimer > msToiletTime)
        {
            PassivePowerUp.Toilet3SpeeUp = false;
            toggle = true;
        }
        else if (toiletTimer <= msToiletTime)
        {
            secForToiletAll += (1 * Time.deltaTime);
            toiletTimer = secForToiletAll;
            float secondsLeft = (msToiletTime - toiletTimer);
            PassivePowerUp.Toilet3SpeeUp = true;
            toggle = false;
        }
    }

    public void AddSand()
    {
        toiletTimer = 0;
        secForToiletAll = 0;
        PlayerPrefs.SetString(timeWhenToiletCleanedPref, System.DateTime.Now.ToBinary().ToString());
        Debug.Log(" Toilet EVENT AddSand in PowerForToilet ----------------------------------- Sand ADDED");
        PlayerPrefs.SetString("timeWhenRepairObjectOrToiletCleanedPref3", System.DateTime.Now.ToBinary().ToString());
    }

    public void Save()
    {
        PlayerPrefs.SetString(exitTimeToiletPref, System.DateTime.Now.ToBinary().ToString());
        Debug.Log(" Before Save toiletTimer3 " + toiletTimer + "  And msToiletTime is  -  " + msToiletTime);
        if (toiletTimer > msToiletTime)
        {
            PlayerPrefs.SetFloat(secondsLeftToiletPref, msToiletTime);
            Debug.Log(" Save to secondsLeftToiletPref in Toilet3 " + msToiletTime);
        }
        else
        {
            PlayerPrefs.SetFloat(secondsLeftToiletPref, toiletTimer);
            Debug.Log(" Save to secondsLeftToiletPref in Toilet3 " + toiletTimer);
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
        if (!PlayerPrefs.HasKey("firstInitPowerForToilet3"))
        {
            toiletTimer = 0;
            secondsLeft = msToiletTime;
            PlayerPrefs.SetInt("firstInitPowerForToilet3", 1);
            PlayerPrefs.SetFloat(secondsLeftToiletPref, toiletTimer);
            // Debug.Log("!________________________________________________________ PlayerPrefs FirstInitPowerForToilet3 SecondsLeft " + secondsLeft);
            PlayerPrefs.SetString(timeWhenToiletCleanedPref, System.DateTime.Now.ToBinary().ToString());
            PlayerPrefs.SetString("timeWhenRepairObjectOrToiletCleanedPref3", System.DateTime.Now.ToBinary().ToString());
        }
        else
        {
            toiletTimer = PlayerPrefs.GetFloat(secondsLeftToiletPref);
            Debug.Log("!________________________________________________________ Load to ToiletTimer3 " + toiletTimer);
            secondsLeft = msToiletTime - (toiletTimer + secAfterExit);
            if (secondsLeft < 0)
            {
                secondsLeft = 0;
            }
            Debug.Log("!________________________________________________________ Load to SecondsToiletLeft3 " + secondsLeft);
            var toiletSecondsWhenWereExit = msToiletTime - toiletTimer - secondsLeft;
            Debug.Log("!________________________________________________________ Load to secondsToiletLeft " + toiletSecondsWhenWereExit);
            secondsWhenWereExit = (int)toiletSecondsWhenWereExit;
        }

        toiletTimer = msToiletTime - secondsLeft;
        Debug.Log("!________________________________________________________ Load to ToiletTimer3 " + toiletTimer);
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
                        PassivePowerUp.toiletPoints3WhenWereExit = pointsWhenWereExit;
                        Debug.Log("Toilet Points when you was absent Timer NotFinished " + pointsWhenWereExit + " to Cat " + catPower.name);
                    }
                }
            }
        }
        if (secAfterExit > secondsWhenWereExit)
        {
            Debug.Log(" Toilet3 if secAfterExit " + secAfterExit + " > secondsToiletLeft " + secondsWhenWereExit);
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
                            PassivePowerUp.toiletPoints3WhenWereExit = pointsWhenWereExit;
                            Debug.Log("Toilet Points when you was absent Timer Finished " + pointsWhenWereExit + " to Cat " + catPower.name + " Where exit time "
                            + secAfterExit + " and Toilet Timer left - " + secondsWhenWereExit);
                        }
                    }
                }
            }
            else if ((int)secondsWhenWereExit == 0)
            {
                PassivePowerUp.toiletPoints3WhenWereExit = 0;
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
                Debug.Log("Load OnOnFocus POwerToilet3 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }
        else
        {
            Save();
            Debug.Log("Save OnOnFocus POwerToilet3 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Save();
            Debug.Log("Save OnOnPause POwerToilet3 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        else
        {
            if (saved)
            {
                Load();
                Debug.Log("Load OnOnPause POwerToilet3 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }
    }

    private void OnDisable()
    {
        TimerForToilet.Toilet3AddSand -= AddSand;
        StopCoroutine(DebugLogToiletSec());
    }

    private void OnDestroy()
    {
        Save();
        Debug.Log("Save OnDestroy POwerToilet3 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
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
                Debug.Log("toiletTimer3         " + toiletTimer);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}