using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScore : MonoBehaviour
{
    public Text scoreWinText;
    private ScoreManager sm;
    private int score;
    void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
    }
    void Update()
    {
        StartCoroutine("Counter");
    }
    IEnumerator Counter()
    {
        for (int i = 1; i <= sm.score; i += 5)
        {
            scoreWinText.text = i.ToString();

            yield return null;
        }
        scoreWinText.text = sm.score.ToString();
    }

}