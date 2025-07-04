using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SpecialOffersTimer : MonoBehaviour
{
    #region EDITOR FIELDS
    [SerializeField] private GameObject moneyPack7;
    [SerializeField] private GameObject buyCoins;
    [SerializeField] private Text moneyPack7TimerText;
    [SerializeField] private Text buyCoinsTimerText;
    [SerializeField] private int timerTime;
    [SerializeField] private int activationHour;
    [SerializeField] private int activationMinute;
    [SerializeField] private int activationSecond;
    #endregion

    #region PRIVATE FIELDS
    private DateTime startTime;
    private DateTime endTime;
    private DateTime lastExecutionDate;
    #endregion

    private void Awake()
    {
        if (PlayerPrefs.HasKey("DateOfLastSpecialOffer"))
        {
            string dateString = PlayerPrefs.GetString("DateOfLastSpecialOffer");
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
            if (day % 2 != 0 && hour >= activationHour && minute >= activationMinute && second >= activationSecond)
            {
                ActivateButton();              
            }
        }
    }

    private void ActivateButton()
    {
        int randomChoice = Random.Range(0, 2);

        if (randomChoice == 0)
        {
            buyCoins.SetActive(true);
            StartCoroutine(UpdateBuyCoinsTimer());
        }
        else
        {
            moneyPack7.SetActive(true);
            StartCoroutine(UpdateEnergyRecoveryTimer());
        }
    }

    private void HideButton()
    {
        moneyPack7.SetActive(false);
        buyCoins.SetActive(false);
    }

    IEnumerator UpdateEnergyRecoveryTimer()
    {
        while (true)
        {
            TimeSpan timeRemaining = endTime - DateTime.Now;

            moneyPack7TimerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeRemaining.Hours, timeRemaining.Minutes, timeRemaining.Seconds);

            if (timeRemaining.Ticks <= 0)
            {
                HideButton();
                yield break;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator UpdateBuyCoinsTimer()
    {
        while (true)
        {
            TimeSpan timeRemaining = endTime - DateTime.Now;

            buyCoinsTimerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeRemaining.Hours, timeRemaining.Minutes, timeRemaining.Seconds);

            if (timeRemaining.Ticks <= 0)
            {
                HideButton();
                yield break;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void SpecialOfferPurchased()
    {
        lastExecutionDate = DateTime.Now;
        string dateString = lastExecutionDate.ToString();
        PlayerPrefs.SetString("DateOfLastSpecialOffer", dateString);
        HideButton();
    }
}
