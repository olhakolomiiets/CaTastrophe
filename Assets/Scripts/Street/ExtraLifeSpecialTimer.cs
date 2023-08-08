using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifeSpecialTimer : MonoBehaviour
{
    private const int _day = 3;
    private DateTime lastVisitDate;
    private int visitCount;

    [SerializeField] private GameObject _extraLifeWindow;

    [SerializeField] private UserCommunicationSO userCommunicationSO;

    private void Start()
    {
        if (PlayerPrefs.HasKey("UserLastVisitDateToGetExtraLife"))
        {
            string dateString = PlayerPrefs.GetString("UserLastVisitDateToGetExtraLife");
            lastVisitDate = DateTime.Parse(dateString);
        }

        DateTime currentDate = DateTime.Now.Date;
        visitCount = PlayerPrefs.GetInt("UserVisitCountToGetExtraLife");

        if (currentDate != lastVisitDate)
        {
            lastVisitDate = currentDate;

            string dateString = lastVisitDate.ToString();
            PlayerPrefs.SetString("UserLastVisitDateToGetExtraLife", dateString);

            visitCount++;
            PlayerPrefs.SetInt("UserVisitCountToGetExtraLife", visitCount);
        }

        if (visitCount >= _day)
        {
            if (userCommunicationSO.OfferExtraLife == 2 && PlayerPrefs.GetInt("extraLife") == 0)
            {
                _extraLifeWindow.SetActive(true);
            }
        }
    }

    public void CloseExtraLifeSpecial()
    {
        PlayerPrefs.SetInt("UserVisitCountToGetExtraLife", 0);
    }
}
