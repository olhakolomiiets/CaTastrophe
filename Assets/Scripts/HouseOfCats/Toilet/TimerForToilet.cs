using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TimerForToilet : MonoBehaviour, IClickable
{
    public float msToiletTime = 60f;
    public GameObject toiletButtonYes;
    public GameObject shitIco;
    public GameObject toiletDirtyImg;
    public GameObject toiletFullImg;
    public Slider toiletSlider;
    public int toiletTimer;
    public bool isCleaned;
    private bool setCleaned;
    public int floor;
    public IToiletInterface toiletPowerComponent;
    public static event PowerForToilet.Toilet1CleanUpDelegate ToiletAddSand;
    public static event PowerForToilet2.Toilet2CleanUpDelegate Toilet2AddSand;
    public static event PowerForToilet3.Toilet3CleanUpDelegate Toilet3AddSand;

    private void Awake()
    {
        Application.runInBackground = false;
        ConnectPowerForToilet();
    }

    private void OnEnable()
    {
        toiletSlider.maxValue = msToiletTime;
    }

    void Start()
    {
        ConnectPowerForToilet();
        PlayerPrefs.SetInt("ToiletInHousePref", 1); // Find out For what this shit, and delete if for nothing
    }
    void Update()
    {
        if (toiletPowerComponent.ToiletTimer() > msToiletTime)
        {
            if (!setCleaned)
            {
                toiletButtonYes.SetActive(true);
                shitIco.SetActive(true);
                toiletDirtyImg.SetActive(true);
                toiletFullImg.SetActive(false);
                isCleaned = false;
                setCleaned = true;
            }
        }
        else
        {
            toiletSlider.value = toiletPowerComponent.ToiletTimer();
            toiletFullImg.SetActive(true);
            if (setCleaned)
            {
                toiletButtonYes.SetActive(false);
                shitIco.SetActive(false);
                toiletDirtyImg.SetActive(false);
                toiletFullImg.SetActive(true);
                isCleaned = true;
                setCleaned = false;
            }
        }
    }

    public void Click()
    {
        setCleaned = false;
        toiletTimer = 0;
        SendAddSandEvent();

        PlayerPrefs.SetInt("stuffToDestroy2" + floor, 0);
        PlayerPrefs.SetInt("stuffToDestroy1" + floor, 0);
        PlayerPrefs.SetInt("stuffToDestroy3" + floor, 0);
        PlayerPrefs.SetInt("stuffToDestroy4" + floor, 0);
        PlayerPrefs.SetInt("stuffToDestroy5" + floor, 0);
        PlayerPrefs.SetInt("stuffToDestroy6" + floor, 0);
    }

    public void Clean()
    {
        isCleaned = true;
        SoundManager.snd.PlayRepairSoft();
        toiletButtonYes.SetActive(false);
        shitIco.SetActive(false);
        toiletDirtyImg.SetActive(false);
        toiletFullImg.SetActive(false);
    }

    public void FillToilet()
    {
        toiletFullImg.SetActive(true);
    }

    private void SendAddSandEvent()
    {
        if (floor == 2)
        {
            Toilet2AddSand?.Invoke();
        }
        else if (floor == 3)
        {
            Toilet3AddSand?.Invoke();
        }
        else ToiletAddSand?.Invoke();
    }

    private void ConnectPowerForToilet()
    {
        if (floor == 2)
        {
            toiletPowerComponent = GameObject.FindObjectOfType<PowerForToilet2>();
        }
        else if (floor == 3)
        {
            toiletPowerComponent = GameObject.FindObjectOfType<PowerForToilet3>();
        }
        else toiletPowerComponent = GameObject.FindObjectOfType<PowerForToilet>();
    }
}
