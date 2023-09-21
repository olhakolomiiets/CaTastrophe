using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameStars : MonoBehaviour
{
    [SerializeField] private MiniGamesSO _miniGame;

    [Space(5)]
    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;

    [Space(5)]
    [SerializeField] private Text textForStar1;
    [SerializeField] private Text textForStar2;
    [SerializeField] private Text textForStar3;

    [Space(5)]
    [SerializeField] private string _bestResultPrefs;
    [SerializeField] private Text bestResultText;
    [SerializeField] private Text _text;

    [Space(5)]
    public int levelIndex;
    //public int starsToUnlock;

    void Start()
    {
        textForStar1.text = _miniGame.Star1.ToString();
        textForStar2.text = _miniGame.Star2.ToString();
        textForStar3.text = _miniGame.Star3.ToString();

        bestResultText.text = $"{PlayerPrefs.GetInt(_bestResultPrefs)} {_text.text}";


        if (GetLevelScore() >= _miniGame.Star1)
        {
            star1.SetActive(true);
            if (PlayerPrefs.GetInt("LevelStar1" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar1" + levelIndex, 1);
            }
        }
        if (GetLevelScore() >= _miniGame.Star2)
        {
            star2.SetActive(true);
            if (PlayerPrefs.GetInt("LevelStar2" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar2" + levelIndex, 1);
            }
        }
        if (GetLevelScore() >= _miniGame.Star3)
        {
            star3.SetActive(true);
            if (PlayerPrefs.GetInt("LevelStar3" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar3" + levelIndex, 1);
            }
        }
    }
    public int GetLevelScore()
    {
        if (PlayerPrefs.HasKey("LevelStars" + levelIndex))
        {
            return PlayerPrefs.GetInt("LevelStars" + levelIndex);
        }
        else
        {
            return 0;
        }
    }

}
