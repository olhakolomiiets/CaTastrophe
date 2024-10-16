﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StarsLevels : MonoBehaviour
{
    // public int scoresForStar1;
    // public int scoresForStar2;
    // public int scoresForStar3;
    [SerializeField] private HouseStarsSO houseStars;
    [SerializeField] private Text textForStar1;
    [SerializeField] private Text textForStar2;
    [SerializeField] private Text textForStar3;
    public int levelIndex;
    public int starsToUnlock;
    private GameObject star1;
    private GameObject star2;
    private GameObject star3;
    public GameObject startButton;
    public GameObject message;
    void Start()
    {
        textForStar1.text = houseStars.Star1.ToString();
        textForStar2.text = houseStars.Star2.ToString();
        textForStar3.text = houseStars.Star3.ToString();
        if (GetLevelScore() >= houseStars.Star1)
        {
            star1 = this.gameObject.transform.GetChild(0).GetChild(3).gameObject;
            star1.SetActive(true);
            if (PlayerPrefs.GetInt("LevelStar1" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar1" + levelIndex, 1);
                PlayerPrefs.SetInt("AllStars", PlayerPrefs.GetInt("AllStars") + 1);
            }
        }
        if (GetLevelScore() >= houseStars.Star2)
        {
            star2 = this.gameObject.transform.GetChild(0).GetChild(5).gameObject;
            star2.SetActive(true);
            if (PlayerPrefs.GetInt("LevelStar2" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar2" + levelIndex, 1);
                PlayerPrefs.SetInt("AllStars", PlayerPrefs.GetInt("AllStars") + 1);
            }
        }
        if (GetLevelScore() >= houseStars.Star3)
        {
            star3 = this.gameObject.transform.GetChild(0).GetChild(7).gameObject;
            star3.SetActive(true);
            if (PlayerPrefs.GetInt("LevelStar3" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar3" + levelIndex, 1);
                PlayerPrefs.SetInt("AllStars", PlayerPrefs.GetInt("AllStars") + 1);
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
    void Update()
    {
        if (PlayerPrefs.GetInt("AllStars") < starsToUnlock )
            {
               
               startButton.SetActive(false);
               message.SetActive(true);
            }
            else{
                startButton.SetActive(true);
                message.SetActive(false);
            }
    }
}
