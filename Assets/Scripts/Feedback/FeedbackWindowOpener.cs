using System;
using UnityEngine;

public class FeedbackWindowOpener : MonoBehaviour
{
    private const int _day = 5;
    private DateTime lastVisitDate;
    private int visitCount;

    [SerializeField] private GameObject feedbackWindow;

    [SerializeField] private FeedbackSO feedbackSO;

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
            if (feedbackSO.AskForFeedback == 1 && PlayerPrefs.GetInt("UserLeftFeedback") == 0)
            {
                feedbackWindow.SetActive(true);
            }
        }
    }

    public void UserLeftFeedback()
    {
        PlayerPrefs.SetInt("UserLeftFeedback", 1);
    }

}

