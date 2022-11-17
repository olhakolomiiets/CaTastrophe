using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [SerializeField]
    public Text timeDisplay;
    public float timer = 90;
    private DateTime timerEnd;
    bool timerRunning = false;
    private float timeDelta = 0.0f;
    private ScoreManager sm;
    private bool TimeUp;
    int sceneIndex;
    int levelComplete;
    public int scoreForWin;
    public GameObject damageImage;
    public GameObject canvas;
    private Animator anim;
    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelComplete = PlayerPrefs.GetInt("LevelComplete");
        Debug.Log("sceneIndex is" + sceneIndex);
        sm = FindObjectOfType<ScoreManager>();
        TimeUp = false;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        damageImage = canvas.gameObject.transform.GetChild(10).gameObject;
        anim = gameObject.GetComponent<Animator>();
    }
    public int GetLevelScore()
    {
        if (PlayerPrefs.HasKey("LevelStars" + sceneIndex))
        {
            return PlayerPrefs.GetInt("LevelStars" + sceneIndex);
        }
        else
        {
            Debug.Log("return 000000000000000000000000");
            return 0;
        }
    }
    private void Update()
    {
        timeDelta = Time.timeSinceLevelLoad - timer;
        TimeSpan t = TimeSpan.FromSeconds(timeDelta);
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
                Debug.Log(PlayerPrefs.GetInt("LevelStars" + sceneIndex, sm.score));
                Debug.Log("sceneIndex is" + sceneIndex);
            }
            if (sceneIndex == 13)
            {
                Invoke("LoadMenuLevels", 1f);
            }
            else
            {
                if (sm.score >= scoreForWin && levelComplete < sceneIndex)
                {
                    PlayerPrefs.SetInt("LevelComplete", sceneIndex);
                }
            }
            timeDisplay.text = ("00:00");
            Camera.main.GetComponent<UIManager>().TimeUp();
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
}