using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdBonusTimer : MonoBehaviour
{
    #region EDITOR FIELDS
    [SerializeField] private GameObject getCoinsButton;
    [SerializeField] private Text timerText;
    [SerializeField] private int timerTime;
    [SerializeField] private int activationHour;
    #endregion

    #region PRIVATE FIELDS
    private int activationMinute;
    private int activationSecond;
    private DateTime startTime;
    private DateTime endTime;
    private DateTime lastExecutionDate;
    #endregion

    private void Awake()
    {
        if (PlayerPrefs.HasKey("lastExecutionDate"))
        {
            string dateString = PlayerPrefs.GetString("lastExecutionDate");
            lastExecutionDate = DateTime.Parse(dateString);
        }
    }

    private void Start()
    {
        DateTime now = DateTime.Now;
        int day = now.Day;
        int hour = now.Hour;
        int minute = now.Minute;
        int second = now.Second;

        startTime = new DateTime(now.Year, now.Month, now.Day, activationHour, activationMinute, activationSecond);
        endTime = startTime.AddMinutes(timerTime);

        if (lastExecutionDate.Date == DateTime.Today)
        {
            HideButton();
        }
        else
        {
            if (day % 2 == 0 && hour >= activationHour && minute >= activationMinute && second >= activationSecond && PlayerPrefs.GetInt("FirstShop") == 1)
            {
                ActivateButton();              
            }
        }
    }

    private void ActivateButton()
    {
        getCoinsButton.SetActive(true);
        StartCoroutine(UpdateTimer());
    }
    private void HideButton()
    {
        getCoinsButton.SetActive(false);
    }

    IEnumerator UpdateTimer()
    {
        while (true)
        {
            TimeSpan timeRemaining = endTime - DateTime.Now;

            timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeRemaining.Hours, timeRemaining.Minutes, timeRemaining.Seconds);

            if (timeRemaining.Ticks <= 0)
            {
                HideButton();
                yield break;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void AdViewed()
    {
        lastExecutionDate = DateTime.Now;
        string dateString = lastExecutionDate.ToString();
        PlayerPrefs.SetString("lastExecutionDate", dateString);
        HideButton();
    }

}
