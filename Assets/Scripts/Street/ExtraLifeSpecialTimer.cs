using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifeSpecialTimer : MonoBehaviour
{
    private const int _day = 1;
    private DateTime lastVisitDate;
    private int visitCount;

    [SerializeField] private GameObject _extraLifeWindow;

    [SerializeField] private UserCommunicationSO userCommunicationSO;

    private void Start()
    {
        if (PlayerPrefs.HasKey("DateOfTheUserLastVisit"))
        {
            string dateString = PlayerPrefs.GetString("DateOfTheUserLastVisit");
            lastVisitDate = DateTime.Parse(dateString);
        }

        DateTime currentDate = DateTime.Now.Date;
        visitCount = PlayerPrefs.GetInt("UserVisitCount");

        if (currentDate != lastVisitDate)
        {
            lastVisitDate = currentDate;

            string dateString = lastVisitDate.ToString();
            PlayerPrefs.SetString("DateOfTheUserLastVisit", dateString);

            visitCount++;
            PlayerPrefs.SetInt("UserVisitCount", visitCount);
        }

        if (visitCount >= _day)
        {
            if (userCommunicationSO.OfferExtraLife > 4 && PlayerPrefs.GetInt("extraLife") == 0)
            {
                _extraLifeWindow.SetActive(true);
            }
        }
    }
}
