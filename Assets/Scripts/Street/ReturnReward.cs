using Firebase.Analytics;
using System;
using UnityEngine;

public class ReturnReward : MonoBehaviour
{
    private int TotalScore;
    [SerializeField] private GameObject _rewardWindow;
    [SerializeField] private int _reward;

    private const int _days = 4;
    private DateTime lastVisitDate;

    private DateTime currentDate;



    private void Start()
    {
        if (PlayerPrefs.HasKey("DateOfTheUserLastVisit"))
        {
            string lastVisitDateString = PlayerPrefs.GetString("DateOfTheUserLastVisit");
            lastVisitDate = DateTime.Parse(lastVisitDateString);

            currentDate = DateTime.Now.Date;

            int daysSinceLastVisit = (int)(currentDate - lastVisitDate).TotalDays;

            if (daysSinceLastVisit >= _days && currentDate != lastVisitDate)
            {
                OpenWindow();
            }
        }
    }

    public void ClaimReward()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + _reward;
        PlayerPrefs.SetInt("TotalScore", TotalScore);

        SoundManager.snd.PlaybuySounds();
        _rewardWindow.SetActive(false);

        string actualDate = currentDate.ToString();
        PlayerPrefs.SetString("DateOfTheUserLastVisit", actualDate);

        FirebaseAnalytics.LogEvent(name: "received_return_reward");
    }

    private void OpenWindow()
    {
        _rewardWindow.SetActive(true);
    }
}
