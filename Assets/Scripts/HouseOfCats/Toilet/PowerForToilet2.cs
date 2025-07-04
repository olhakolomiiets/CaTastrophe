using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PowerForToilet2 : MonoBehaviour, IToiletInterface
{
    public static PowerForToilet2 instance;
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
    private bool saved = true;
    [SerializeField] private string skillName;
    [SerializeField] private SkillManager skillManager;
    [SerializeField] private int isToiletActive;
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
        TimerForToilet.Toilet2AddSand += AddSand;
    }

    void Update()
    {
        if (toiletTimer > msToiletTime)
        {
            if (isToiletActive == 2)
            {
                isToiletActive = 1;
                skillManager.UpdateSkill(skillName, isToiletActive);
                PlayerPrefs.SetInt("LitterBox2State", isToiletActive);
                PassivePowerUp.Toilet2SpeeUp = false;
            }
        }
        else if (toiletTimer <= msToiletTime)
        {
            secForToiletAll += (1 * Time.deltaTime);
            toiletTimer = secForToiletAll;
            float secondsLeft = (msToiletTime - toiletTimer);

            if (isToiletActive == 1)
            {
                isToiletActive = 2;
                skillManager.UpdateSkill(skillName, isToiletActive);
                PlayerPrefs.SetInt("LitterBox2State", isToiletActive);
                PassivePowerUp.Toilet2SpeeUp = true;
            }
        }
    }

    public void AddSand()
    {
        toiletTimer = 0;
        secForToiletAll = 0;
        PlayerPrefs.SetString(timeWhenToiletCleanedPref, System.DateTime.Now.ToBinary().ToString());
        PlayerPrefs.SetString("timeWhenRepairObjectOrToiletCleanedPref2", System.DateTime.Now.ToBinary().ToString());

        if (PlayerPrefs.GetInt("LitterBox2State") == 0)
        {
            isToiletActive = 1;
            PlayerPrefs.SetInt("LitterBox2State", isToiletActive);
        }
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
        if (!PlayerPrefs.HasKey("firstInitPowerForToilet2"))
        {
            toiletTimer = 0;
            secondsLeft = msToiletTime;
            PlayerPrefs.SetInt("firstInitPowerForToilet2", 1);
            PlayerPrefs.SetFloat(secondsLeftToiletPref, toiletTimer);
            PlayerPrefs.SetString(timeWhenToiletCleanedPref, System.DateTime.Now.ToBinary().ToString());
            PlayerPrefs.SetString("timeWhenRepairObjectOrToiletCleanedPref2", System.DateTime.Now.ToBinary().ToString());
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

        isToiletActive = PlayerPrefs.GetInt("LitterBox2State");

        secForToiletAll = (float)toiletTimer;
        TimeSpan timer = TimeSpan.FromSeconds(rawTime);
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
            if (saved)
            {
                Load();
            }
        }
    }

    private void OnDisable()
    {
        TimerForToilet.Toilet2AddSand -= AddSand;
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