using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetTotalScore : MonoBehaviour
{
    public int TotalScore;
    private ScoreManager sm;
    public Text scoreTotalText;
    private int allStars;
    public Text allStarsText;
    private void Update()
    {
        // StartCoroutine(GetScore());
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        sm = FindObjectOfType<ScoreManager>();
        scoreTotalText.text = TotalScore.ToString();
        allStars = PlayerPrefs.GetInt("AllStars");
        allStarsText.text = allStars.ToString();
    }
    private IEnumerator GetScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            TotalScore = PlayerPrefs.GetInt("TotalScore");
            sm = FindObjectOfType<ScoreManager>();
            scoreTotalText.text = TotalScore.ToString();
        }
    }
}
