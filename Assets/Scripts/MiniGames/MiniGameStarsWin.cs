using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using Firebase.Analytics;

public class MiniGameStarsWin : MonoBehaviour
{
    [SerializeField] private MiniGamesSO miniGameStars;

    [Space(5)]
    [SerializeField] private MiniGameRewards _miniGameRewards;

    [Space(5)]
    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;

    [Space(5)]
    [SerializeField] private Text textForStar1;
    [SerializeField] private Text textForStar2;
    [SerializeField] private Text textForStar3;

    [Space(5)]
    [SerializeField] private Text bestResultText;

    [Space(5)]
    public IMiniGamesScore miniGameComponent;
    [SerializeField] GameObject _objectWithInterface;

    [Space(5)]
    [SerializeField] private Text resultText;
    [SerializeField] private Text scoreText;
    [SerializeField] private float multiplier;

    private int _gameScore;
    private int _coins;
    private int _totalScore;

    [SerializeField] private bool isRoofRunner;

    private void OnEnable()
    {
        miniGameComponent = _objectWithInterface.GetComponents<Component>().OfType<IMiniGamesScore>().FirstOrDefault();

        _gameScore = miniGameComponent.MiniGameScore();

        _coins = (int)(_gameScore * multiplier);

        StartCoroutine("Counter");
        StartCoroutine("CounterTotal");

        _totalScore = PlayerPrefs.GetInt("TotalScore");
        _totalScore = _totalScore + _coins;
        PlayerPrefs.SetInt("TotalScore", _totalScore);
    }

    void Start()
    {
        textForStar1.text = miniGameStars.Star1.ToString();
        textForStar2.text = miniGameStars.Star2.ToString();
        textForStar3.text = miniGameStars.Star3.ToString();        

        if (PlayerPrefs.GetInt("LevelStars" + miniGameStars.LevelIndex) < _gameScore)
            PlayerPrefs.SetInt("LevelStars" + miniGameStars.LevelIndex, _gameScore);

        if (_gameScore >= miniGameStars.Star1)
        {
            miniGameStars.SetCatMood(true);
            FirebaseAnalytics.LogEvent(name: "star1_miniGame_" + miniGameStars.LevelIndex);
            StartCoroutine(StarWin1());           
        }
        else miniGameStars.SetCatMood(false);

        if (_gameScore >= miniGameStars.Star2)
        {
            FirebaseAnalytics.LogEvent(name: "star2_miniGame_" + miniGameStars.LevelIndex);
            StartCoroutine(StarWin2());
        }

        if (_gameScore >= miniGameStars.Star3)
        {
            FirebaseAnalytics.LogEvent(name: "star3_miniGame_" + miniGameStars.LevelIndex);
            StartCoroutine(StarWin3());
        }

        if (GetLevelScore() >= miniGameStars.Star1)
        {
            if (PlayerPrefs.GetInt("LevelStar1" + miniGameStars.LevelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar1" + miniGameStars.LevelIndex, 1);
                miniGameStars.SetIsReward1Show(true);
            }
            else miniGameStars.SetIsReward1Show(false);
        }

        if (GetLevelScore() >= miniGameStars.Star2)
        {
            if (PlayerPrefs.GetInt("LevelStar2" + miniGameStars.LevelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar2" + miniGameStars.LevelIndex, 1);
                miniGameStars.SetIsReward2Show(true);
            }
            else miniGameStars.SetIsReward2Show(false);
        }

        if (GetLevelScore() >= miniGameStars.Star3)
        {
            if (PlayerPrefs.GetInt("LevelStar3" + miniGameStars.LevelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar3" + miniGameStars.LevelIndex, 1);
                miniGameStars.SetIsReward3Show(true);
            }
            else miniGameStars.SetIsReward3Show(false);
        }

        bestResultText.text = $"{PlayerPrefs.GetInt(miniGameStars.BestResultPrefs)}";

        OnActiveMiniGameRewards();
    }

    private void OnActiveMiniGameRewards()
    {
        _miniGameRewards.gameObject.SetActive(true);
    }
    public int GetLevelScore()
    {
        if (PlayerPrefs.HasKey("LevelStars" + miniGameStars.LevelIndex))
        {
            return PlayerPrefs.GetInt("LevelStars" + miniGameStars.LevelIndex);
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

    IEnumerator Counter()
    {
        if (isRoofRunner)
        {
            for (int i = 0; i <= _gameScore; i += 8)
            {
                resultText.text = i.ToString();

                yield return null;
            }
        }
        else
        {
            for (int i = 0; i <= _gameScore; i += 1)
            {
                resultText.text = i.ToString();

                yield return null;
            }
        }
        resultText.text = _gameScore.ToString();
    }

    IEnumerator CounterTotal()
    {
        for (int i = 0; i <= _coins; i += 1)
        {
            scoreText.text = i.ToString();

            yield return null;
        }
        scoreText.text = _coins.ToString();
    }

    private void OnDisable()
    {
        StopCoroutine("Counter");
        StopCoroutine("CounterTotal");
    }
}
