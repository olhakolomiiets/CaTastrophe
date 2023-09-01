using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallsStarsWin : MonoBehaviour
{
    [SerializeField] private BallsStarsSO ballsStars;
    public int levelIndex;
    private GameObject star1;
    private GameObject star2;
    private GameObject star3;
    private ScoreManager sm;
    [SerializeField] private Text textForStar1;
    [SerializeField] private Text textForStar2;
    [SerializeField] private Text textForStar3;

    [SerializeField] private BasketBallLogic basketBallLogic;

    void Start()
    {
        sm = FindObjectOfType<ScoreManager>();
        basketBallLogic = FindObjectOfType<BasketBallLogic>();
        star1 = this.gameObject.transform.GetChild(1).gameObject;
        star2 = this.gameObject.transform.GetChild(4).gameObject;
        star3 = this.gameObject.transform.GetChild(7).gameObject;
        textForStar1.text = ballsStars.Star1.ToString();
        textForStar2.text = ballsStars.Star2.ToString();
        textForStar3.text = ballsStars.Star3.ToString();

        if (basketBallLogic.ballsScored >= ballsStars.Star1)
        {
            StartCoroutine(StarWin1());
        }
        if (basketBallLogic.ballsScored >= ballsStars.Star2)
        {
            StartCoroutine(StarWin2());
        }
        if (basketBallLogic.ballsScored >= ballsStars.Star3)
        {
            StartCoroutine(StarWin3());
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
