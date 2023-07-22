using System;
using UnityEngine;

public class FeedbackWindowOpener : MonoBehaviour
{
    private const int _day = 2;
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
            Debug.Log("     oOo -----     Last Visit Date     ----- oOo     " + lastVisitDate);
        }

        DateTime currentDate = DateTime.Now.Date;
        Debug.Log("     oOo -----     Current Date     ----- oOo     " + currentDate);

        if (currentDate != lastVisitDate)
        {
            lastVisitDate = currentDate;
            Debug.Log("     oOo -----     Last Visit Date if currentDate != lastVisitDate     ----- oOo     " + lastVisitDate);

            string dateString = lastVisitDate.ToString();
            PlayerPrefs.SetString("DateOfTheUserLastVisit", dateString);
            Debug.Log("     oOo -----     Last Visit Date     ----- oOo     " + PlayerPrefs.GetString("DateOfTheUserLastVisit"));

            visitCount = PlayerPrefs.GetInt("UserVisitCount");
            visitCount++;
            PlayerPrefs.SetInt("UserVisitCount", visitCount);
            Debug.Log("     oOo -----     Visit Count     ----- oOo     " + visitCount);
        }

        if (visitCount >= _day)
        {
            if (feedbackSO.AskForFeedback == 1 && PlayerPrefs.GetInt("UserLeftFeedback") == 0)
            {
                feedbackWindow.SetActive(true);

                Debug.Log("     oOo -----     Feedback Window Open     ----- oOo     ");
            }
        }
    }

    public void UserLeftFeedback()
    {
        PlayerPrefs.SetInt("UserLeftFeedback", 1);
    }

}

