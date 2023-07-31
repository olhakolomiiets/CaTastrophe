using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarterPackTimer : MonoBehaviour
{
    [SerializeField] private GameObject starterPack;
    [SerializeField] private GameObject starterPackWindow;
    [SerializeField] private Text starterPackTimerText;

    [SerializeField] private int timerTime;

    [SerializeField] private int activationHour;
    private int activationMinute;
    private int activationSecond;

    private DateTime _startTime;
    private DateTime _endTime;

    private DateTime _lastVisitDate;
    [SerializeField] private int _visitCount;

    private int _days = 2;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("StarterPackDate"))
        {
            string dateString = PlayerPrefs.GetString("StarterPackDate");
            _lastVisitDate = DateTime.Parse(dateString);
        }
    }

    private void Start()
    {
        DateTime now = DateTime.Now;
        int day = now.Day;
        int hour = now.Hour;
        int minute = now.Minute;
        int second = now.Second;


        _startTime = new DateTime(now.Year, now.Month, now.Day, activationHour, activationMinute, activationSecond);
        _endTime = _startTime.AddHours(timerTime);

        DateTime currentDate = DateTime.Now.Date;

        _visitCount = PlayerPrefs.GetInt("UserVisitCountToGetStarterPack");

        if (currentDate != _lastVisitDate)
        {
            _lastVisitDate = currentDate;

            string dateString = _lastVisitDate.ToString();
            PlayerPrefs.SetString("StarterPackDate", dateString);
            
            _visitCount++;
            PlayerPrefs.SetInt("UserVisitCountToGetStarterPack", _visitCount);
        }

        if (_visitCount >= _days)
        {
            if (PlayerPrefs.GetInt("UserGotStarterPack") == 0)
            {
                ActivateButton();
            }
            else
            {
                HideButton();
            }
        }
    }

    private void ActivateButton()
    {
        if (PlayerPrefs.HasKey("StarterPackFirstOpen") == false)
        {
            starterPackWindow.SetActive(true);
            PlayerPrefs.SetInt("StarterPackFirstOpen", 1);
        }
        starterPack.SetActive(true);
        StartCoroutine(UpdateStarterPackTimer());      
        
    }
    public void HideButton()
    {
        starterPack.SetActive(false);
    }

    IEnumerator UpdateStarterPackTimer()
    {
        while (true)
        {
            TimeSpan timeRemaining = _endTime - DateTime.Now;

            starterPackTimerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", (int) timeRemaining.TotalHours, timeRemaining.Minutes, timeRemaining.Seconds);

            if (timeRemaining.Ticks <= 0)
            {
                HideButton();
                yield break;
            }

            yield return new WaitForSeconds(1f);
        }
    }

}
