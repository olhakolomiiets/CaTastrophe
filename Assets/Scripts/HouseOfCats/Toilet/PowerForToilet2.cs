using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PowerForToilet2 : MonoBehaviour, IToiletInterface
{
    public static PowerForToilet2 instance;
    [SerializeField] private List<FloatSO> catPowersSO;
    [SerializeField] private ToiletTimerSO _toiletTimerSO;
    public string timeWhenToiletCleanedPref;
    public string exitTimeToiletPref;
    public string secondsLeftToiletPref;
    public string ToiletUpgradePrefFloor;
    public float msToiletTime;
    public float toiletTimer = 0;
    public float secondsWhenWereExit;
    public float secondsLeft;
    public int secAfterExit;
    public float secForToiletAll;
    public float pointsWhenWereExit;
    private bool toggle;
    private bool saved = true;
    public delegate void Toilet2CleanUpDelegate();

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
            msToiletTime = _toiletTimerSO.MsToiletTimeLvl1;
        }
        if (purch == 1)
        {
            msToiletTime = _toiletTimerSO.MsToiletTimeLvl2;
        }
        if (purch == 2)
        {
            msToiletTime = _toiletTimerSO.MsToiletTimeLvl3;
        }
    }
    void Start()
    {
        StartCoroutine(DebugLogToiletSec());
        TimerForToilet.Toilet2AddSand += AddSand;
        Debug.Log("SUB");
    }

    void Update()
    {
        if (toiletTimer > msToiletTime)
        {
            PassivePowerUp.Toilet2SpeeUp = false;
            toggle = true;
        }
        else if (toiletTimer <= msToiletTime)
        {
            secForToiletAll += (1 * Time.deltaTime);
            toiletTimer = secForToiletAll;
            float secondsLeft = (msToiletTime - toiletTimer);
            PassivePowerUp.Toilet2SpeeUp = true;
            toggle = false;
        }
    }

    public void AddSand()
    {
        toiletTimer = 0;
        secForToiletAll = 0;
        PlayerPrefs.SetString(timeWhenToiletCleanedPref, System.DateTime.Now.ToBinary().ToString());
        Debug.Log(" Toilet2 EVENT AddSand in PowerForToilet ----------------------------------- Sand ADDED");
        PlayerPrefs.SetString("timeWhenRepairObjectOrToiletCleanedPref2", System.DateTime.Now.ToBinary().ToString());
    }

    public void Save()
    {
        PlayerPrefs.SetString(exitTimeToiletPref, System.DateTime.Now.ToBinary().ToString());
        Debug.Log(" Before Save toiletTimer2 " + toiletTimer + "  And msToiletTime is  -  " + msToiletTime);
        if (toiletTimer > msToiletTime)
        {
            PlayerPrefs.SetFloat(secondsLeftToiletPref, msToiletTime);
            Debug.Log(" Save to secondsLeftToiletPref in Toilet2 " + msToiletTime);
        }
        else if (toiletTimer < msToiletTime)
        {
            PlayerPrefs.SetFloat(secondsLeftToiletPref, toiletTimer);
            Debug.Log(" Save to secondsLeftToiletPref in Toilet2 " + toiletTimer);
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
        if (!PlayerPrefs.HasKey("firstInitPowerForToilet2"))
        {
            toiletTimer = 0;
            secondsLeft = msToiletTime;
            PlayerPrefs.SetInt("firstInitPowerForToilet2", 1);
            PlayerPrefs.SetFloat(secondsLeftToiletPref, toiletTimer);
            // Debug.Log("!________________________________________________________ PlayerPrefs FirstInitPowerForToilet2 SecondsLeft " + secondsLeft);
            PlayerPrefs.SetString(timeWhenToiletCleanedPref, System.DateTime.Now.ToBinary().ToString());
            PlayerPrefs.SetString("timeWhenRepairObjectOrToiletCleanedPref2", System.DateTime.Now.ToBinary().ToString());
        }
        else
        {
            toiletTimer = PlayerPrefs.GetFloat(secondsLeftToiletPref);
            Debug.Log("!________________________________________________________ Load to ToiletTimer2 " + toiletTimer);
            secondsLeft = msToiletTime - (toiletTimer + secAfterExit);
            if (secondsLeft < 0)
            {
                secondsLeft = 0;
            }
            Debug.Log("!________________________________________________________ Load to SecondsToiletLeft2 " + secondsLeft);
            var toiletSecondsWhenWereExit = msToiletTime - toiletTimer - secondsLeft;
            Debug.Log("!________________________________________________________ Load to secondsToiletLeft " + toiletSecondsWhenWereExit);
            secondsWhenWereExit = (int)toiletSecondsWhenWereExit;
        }

        toiletTimer = msToiletTime - secondsLeft;
        Debug.Log("!________________________________________________________ Load to ToiletTimer2 " + toiletTimer);
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
                        PassivePowerUp.toiletPoints2WhenWereExit = pointsWhenWereExit;
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
                            PassivePowerUp.toiletPoints2WhenWereExit = pointsWhenWereExit;
                            Debug.Log("Toilet Points when you was absent Timer Finished " + pointsWhenWereExit + " to Cat " + catPower.name + " Where exit time "
                            + secAfterExit + " and Toilet Timer left - " + secondsWhenWereExit);
                        }
                    }
                }
            }
            else if ((int)secondsWhenWereExit == 0)
            {
                PassivePowerUp.toiletPoints2WhenWereExit = 0;
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
                Debug.Log("Load OnOnFocus POwerToilet2 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }
        else
        {
            Save();
            Debug.Log("Save OnOnFocus POwerToilet2 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Save();
            Debug.Log("Save OnOnPause POwerToilet2 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        else
        {
            if (saved)
            {
                Load();
                Debug.Log("Load OnOnPause POwerToilet2 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }
    }

    private void OnDisable()
    {
        TimerForToilet.Toilet2AddSand -= AddSand;
        StopCoroutine(DebugLogToiletSec());
    }

    private void OnDestroy()
    {
        Save();
        Debug.Log("Save OnDestroy POwerToilet2 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }

    public float ToiletTimer()
    {
        return toiletTimer;
    }

    public float GetMsToiletTime()
    {
        return msToiletTime;
    }

    private IEnumerator DebugLogToiletSec()
    {
        while (true)
        {
            if (!toggle)
            {
                Debug.Log("toiletTimer2         " + toiletTimer);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}