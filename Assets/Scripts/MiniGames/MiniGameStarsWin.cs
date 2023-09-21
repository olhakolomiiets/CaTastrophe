using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class MiniGameStarsWin : MonoBehaviour
{
    [SerializeField] private MiniGamesSO miniGameStars;

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
    [SerializeField] private string _bestResultPrefs;
    [SerializeField] private Text bestResultText;
    [SerializeField] private Text _text;

    [Space(5)]
    public IMiniGamesScore miniGameComponent;
    [SerializeField] GameObject _objectWithInterface;

    private int _gameScore;

    void Start()
    {
        miniGameComponent = _objectWithInterface.GetComponents<Component>().OfType<IMiniGamesScore>().FirstOrDefault();

        _gameScore = miniGameComponent.MiniGameScore();

        textForStar1.text = miniGameStars.Star1.ToString();
        textForStar2.text = miniGameStars.Star2.ToString();
        textForStar3.text = miniGameStars.Star3.ToString();

        bestResultText.text = $"{PlayerPrefs.GetInt(_bestResultPrefs)} {_text.text}";

        PlayerPrefs.SetInt("LevelStars" + levelIndex, _gameScore);

        if (_gameScore >= miniGameStars.Star1)
        {
            StartCoroutine(StarWin1());
        }
        if (_gameScore >= miniGameStars.Star2)
        {
            StartCoroutine(StarWin2());
        }
        if (_gameScore >= miniGameStars.Star3)
        {
            StartCoroutine(StarWin3());
        }

        if (GetLevelScore() >= miniGameStars.Star1)
        {
            if (PlayerPrefs.GetInt("LevelStar1" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar1" + levelIndex, 1);
            }
        }
        if (GetLevelScore() >= miniGameStars.Star2)
        {
            if (PlayerPrefs.GetInt("LevelStar2" + levelIndex) == 0)
            {
                PlayerPrefs.SetInt("LevelStar2" + levelIndex, 1);
            }
        }
        if (GetLevelScore() >= miniGameStars.Star3)
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
