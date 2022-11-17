using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public int score;
    public int TotalScore;
    public int scoreForWin;
    [SerializeField]
    public Text scoreDisplay;
    public Text scoreWinText;
    public Text scoreTimeUpText;
    public Text scoreTotalText;
    bool Finish;
    int sceneIndex;
    public Animator[] scoreAnim;
    public event TimeBonus.TimeBonusDelegate UpdateBonusScore;

    public void Start()
    {
        TotalScore = GetTotalScore();
        Finish = false;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        scoreDisplay.text = score.ToString();
        scoreWinText.text = score.ToString() + "$";
        TotalScore = GetTotalScore();
    }
    public void DestroyBonus(int x)
    {
        score = score + x;
        PlayerPrefs.SetInt("score", score);
        foreach (Animator anim in scoreAnim)
        {
            anim.SetTrigger("Plus");
        }
    }
    public int GetTotalScore()
    {
        if (PlayerPrefs.HasKey("TotalScore"))
        {
            return PlayerPrefs.GetInt("TotalScore");
        }
        else
        {
            return 0;
        }
    }
    public int GetLevelScore()
    {
        if (PlayerPrefs.HasKey("LevelStars" + sceneIndex))
        {
            return PlayerPrefs.GetInt("LevelStars" + sceneIndex, score);
        }
        else
        {
            return 0;
        }
    }

    public void UpdateTimeBonusScore()
    {
        UpdateBonusScore?.Invoke();
    }
}
