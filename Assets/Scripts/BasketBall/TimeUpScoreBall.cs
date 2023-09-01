using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUpScoreBall : MonoBehaviour
{
    public Text scoreTimeUpText;
    public Text scoreTotalText;
    private ScoreManager sm;
    private BasketBallLogic basketBallLogic;
    private int totalScore;

    private void OnEnable()
    {
        sm = FindObjectOfType<ScoreManager>();
        basketBallLogic = FindObjectOfType<BasketBallLogic>();
        totalScore = sm.TotalScore;
        StartCoroutine("Counter");
        StartCoroutine("CounterTotal");
    }

    IEnumerator Counter()
    {
        for (int i = 0; i <= basketBallLogic.ballsScored; i += 1)
        {
            scoreTimeUpText.text = i.ToString();

            yield return null;
        }
        scoreTimeUpText.text = basketBallLogic.ballsScored.ToString();
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

    private void OnDisable() 
    {
        StopCoroutine("Counter");
        StopCoroutine("CounterTotal");
    }
}