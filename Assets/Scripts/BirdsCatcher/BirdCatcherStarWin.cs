using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BirdCatcherStarWin : MonoBehaviour
{
    [SerializeField] private MiniGamesSO ballsStars;

    [Space(5)]
    public int levelIndex;

    [Space(5)]
    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;

    [Space(5)]
    [SerializeField] private Text textForStar1;
    [SerializeField] private Text textForStar2;
    [SerializeField] private Text textForStar3;

    [Space(5)]
    [SerializeField] private BirdsCatcherLogic birdCatcherLogic;

    void Start()
    {
        birdCatcherLogic = FindObjectOfType<BirdsCatcherLogic>();

        textForStar1.text = ballsStars.Star1.ToString();
        textForStar2.text = ballsStars.Star2.ToString();
        textForStar3.text = ballsStars.Star3.ToString();

        PlayerPrefs.SetInt("LevelStars" + levelIndex, birdCatcherLogic.birdsCatched);

        if (birdCatcherLogic.birdsCatched >= ballsStars.Star1)
        {
            StartCoroutine(StarWin1());
        }
        if (birdCatcherLogic.birdsCatched >= ballsStars.Star2)
        {
            StartCoroutine(StarWin2());
        }
        if (birdCatcherLogic.birdsCatched >= ballsStars.Star3)
        {
            StartCoroutine(StarWin3());
        }

        if (GetLevelScore() >= ballsStars.Star1)
        {
            if (PlayerPrefs.GetInt("LevelStar1" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar1" + levelIndex, 1);
            }
        }
        if (GetLevelScore() >= ballsStars.Star2)
        {
            if (PlayerPrefs.GetInt("LevelStar2" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar2" + levelIndex, 1);
            }
        }
        if (GetLevelScore() >= ballsStars.Star3)
        {
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

    IEnumerator StarWin1()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        star1.SetActive(true);
    }

    IEnumerator StarWin2()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        star2.SetActive(true);
    }

    IEnumerator StarWin3()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        star3.SetActive(true);
    }
}
