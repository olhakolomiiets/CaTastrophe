using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerStarsWin : MonoBehaviour
{
    [SerializeField] private RunnerStarsSO runnerStars;
    public int levelIndex;
    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;
    private ScoreManager sm;
    [SerializeField] private Text textForStar1;
    [SerializeField] private Text textForStar2;
    [SerializeField] private Text textForStar3;
    [SerializeField] private UIController uiController;

    void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        star1 = this.gameObject.transform.GetChild(1).gameObject;
        star2 = this.gameObject.transform.GetChild(4).gameObject;
        star3 = this.gameObject.transform.GetChild(7).gameObject;
        textForStar1.text = runnerStars.Star1.ToString();
        textForStar2.text = runnerStars.Star2.ToString();
        textForStar3.text = runnerStars.Star3.ToString();

        PlayerPrefs.SetInt("LevelStars" + levelIndex, uiController.distance);

        if (uiController.distance >= runnerStars.Star1)
        {
            StartCoroutine(StarWin1());
        }
        if (uiController.distance >= runnerStars.Star2)
        {
            StartCoroutine(StarWin2());
        }
        if (uiController.distance >= runnerStars.Star3)
        {
            StartCoroutine(StarWin3());
        }

        if (GetLevelScore() >= runnerStars.Star1)
        {
            if (PlayerPrefs.GetInt("LevelStar1" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar1" + levelIndex, 1);
            }
        }
        if (GetLevelScore() >= runnerStars.Star2)
        {
            if (PlayerPrefs.GetInt("LevelStar2" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar2" + levelIndex, 1);
            }
        }
        if (GetLevelScore() >= runnerStars.Star3)
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
            Debug.Log("return 000000000000000000000000");
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
