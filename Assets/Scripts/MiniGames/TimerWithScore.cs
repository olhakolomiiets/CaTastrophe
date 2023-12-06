using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Analytics;

public class TimerWithScore : MonoBehaviour, IMiniGamesScore
{
    [SerializeField] private Text timeDisplay;
    [SerializeField] private float timer;
    private DateTime timerEnd;
    bool timerRunning = false;
    private float timeDelta = 0.0f;
    private ScoreManager sm;
    [HideInInspector] public bool TimeUp;
    int sceneIndex;
    int levelComplete;
    [SerializeField] private GameObject damageImage;
    [SerializeField] private GameObject canvas;
    private Animator anim;
    private UIManager _uiManager;
    private TimeSpan elapsedTime;

    private bool _isGetExtraTime;

    private float timeLeft;
    [HideInInspector] public int gameScore;

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelComplete = PlayerPrefs.GetInt("LevelComplete");
        Debug.Log("sceneIndex is" + sceneIndex);
        sm = FindObjectOfType<ScoreManager>();
        TimeUp = false;
        anim = gameObject.GetComponent<Animator>();
        _uiManager = Camera.main.GetComponent<UIManager>();

    }
    public int GetLevelScore()
    {
        if (PlayerPrefs.HasKey("LevelStars" + sceneIndex))
        {
            return PlayerPrefs.GetInt("LevelStars" + sceneIndex);
        }
        else
        {
            return 0;
        }
    }
    private void Update()
    {
        timeDelta = Time.timeSinceLevelLoad - timer;
        TimeSpan t = TimeSpan.FromSeconds(timeDelta);
        elapsedTime = t;
        if (timeDelta < 0.0)
        {
            timeDisplay.text = t.ToString(@"mm\:ss");
        }
        if (timeDelta >= 0.0 && TimeUp == false)
        {
            sm.TotalScore += sm.score;
            PlayerPrefs.SetInt("TotalScore", sm.TotalScore);
            if (GetLevelScore() < sm.score)
            {
                PlayerPrefs.SetInt("LevelStars" + sceneIndex, sm.score);
            }
            timeDisplay.text = ("00:00");

            if (!_isGetExtraTime && sceneIndex == 16)
            {
                _uiManager.GetTime();
                _isGetExtraTime = true;
            }
            else _uiManager.TimeUp();

            damageImage.SetActive(false);
            TimeUp = true;
        }
    }
    void LoadMenuLevels()
    {
        SceneManager.LoadScene("menu-levels");
        Time.timeScale = 1;
    }
    public void AddSecondsToTimer(int sec)
    {
        anim.SetTrigger("Add");
        timer = timer + sec;
    }

    public void StopMiniGameTimer()
    {
        timeLeft = timeDelta * -1;
        SetHeadBallScore();

        timeDisplay.text = elapsedTime.ToString(@"mm\:ss");
        _uiManager.TimeUp();
    }

    public void SetHeadBallScore()
    {
        gameScore = (int)timeLeft;
        Debug.Log("!!!!!!!!!!!!!!--------------- Head Ball Score " + gameScore);

        int bestResult = PlayerPrefs.GetInt("HeadBallBestResult");
        int headBallResult = (180 - (int)timeLeft);
        if (headBallResult <= bestResult || bestResult == 0)
        {
            PlayerPrefs.SetInt("HeadBallBestResult", headBallResult);
        }
    }

    public int MiniGameScore()
    {
        return gameScore;
    }

}
