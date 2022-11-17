using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUpScore : MonoBehaviour
{
    public Text scoreTimeUpText;
    public Text scoreTotalText;
    private ScoreManager sm;
    private int score;
    private int totalScore;

    void Start()
    {
        // sm = FindObjectOfType<ScoreManager>();
        // totalScore = sm.TotalScore;
    }
    private void OnEnable()
    {
        sm = FindObjectOfType<ScoreManager>();
        totalScore = sm.TotalScore;
        if (sm.score > PlayerPrefs.GetInt("AwardMoneyPerHouse", 0))
        {
            PlayerPrefs.SetInt("AwardMoneyPerHouse", sm.score);
        }
    }
    void Update()
    {
        StartCoroutine("Counter");
        StartCoroutine("CounterTotal");
    }
    IEnumerator Counter()
    {
        for (int i = 1; i <= sm.score; i += 4)
        {
            scoreTimeUpText.text = i.ToString();

            yield return null;
        }
        scoreTimeUpText.text = sm.score.ToString();

    }

    IEnumerator CounterTotal()
    {
        if (sm.TotalScore > PlayerPrefs.GetInt("AwardTotalMoney"))
        {
            PlayerPrefs.SetInt("AwardTotalMoney", sm.TotalScore);
        }
        for (int i = totalScore - sm.score; i <= sm.TotalScore; i += 4)
        {
            scoreTotalText.text = i.ToString();

            yield return null;
        }
        scoreTotalText.text = sm.TotalScore.ToString();
    }
}