using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PowerForToilet : MonoBehaviour, IToiletInterface
{
    public static PowerForToilet instance;
    [SerializeField] private List<FloatSO> catPowersSO;
    [SerializeField] private ToiletTimerSO _toiletTimerSO;
    public string timeWhenToiletCleanedPref;
    public string exitTimeToiletPref;
    public string secondsLeftToiletPref;
    public string ToiletUpgradePrefFloor;
    public float msToiletTime;
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
        TimerForToilet.ToiletAddSand += AddSand;
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
        PlayerPrefs.SetString("timeWhenRepairObjectOrToiletCleanedPref1", System.DateTime.Now.ToBinary().ToString());
    }

    public void Save()
    {
        PlayerPrefs.SetString(exitTimeToiletPref, System.DateTime.Now.ToBinary().ToString());
        if (toiletTimer > msToiletTime)
        {
            PlayerPrefs.SetFloat(secondsLeftToiletPref, msToiletTime);
        }
        else if (toiletTimer < msToiletTime)
        {
            PlayerPrefs.SetFloat(secondsLeftToiletPref, toiletTimer);
        }
        saved = true;
    }

    public void Load()
    {
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
            PlayerPrefs.SetString(timeWhenToiletCleanedPref, System.DateTime.Now.ToBinary().ToString());
            PlayerPrefs.SetString("timeWhenRepairObjectOrToiletCleanedPref1", System.DateTime.Now.ToBinary().ToString());
        }
        else
        {
            toiletTimer = PlayerPrefs.GetFloat(secondsLeftToiletPref);
            secondsLeft = msToiletTime - (toiletTimer + secAfterExit);
            if (secondsLeft < 0)
            {
                secondsLeft = 0;
            }
            var toiletSecondsWhenWereExit = msToiletTime - toiletTimer - secondsLeft;
            secondsWhenWereExit = (int)toiletSecondsWhenWereExit;
        }

        toiletTimer = msToiletTime - secondsLeft;

        secForToiletAll = (float)toiletTimer;
        TimeSpan timer = TimeSpan.FromSeconds(rawTime);


        if (secAfterExit <= secondsWhenWereExit)
        {
            pointsWhenWereExit = secAfterExit * 0.00083333f;
            if (pointsWhenWereExit > 0)
            {
                foreach (FloatSO catPower in catPowersSO)
                {
                    if (catPower.Value < 10)
                    {
                        PassivePowerUp.toiletPointsWhenWereExit = pointsWhenWereExit;
                    }
                }
            }
        }
        if (secAfterExit > secondsWhenWereExit)
        {
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
            }
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
            if (saved)
            {
                Load();
            }
        }

    }

    private void OnDisable()
    {
        TimerForToilet.ToiletAddSand -= AddSand;
    }

    private void OnDestroy()
    {
        Save();
    }

    public float ToiletTimer()
    {
        return toiletTimer;
    }

    public float GetMsToiletTime()
    {
        return msToiletTime;
    }
}