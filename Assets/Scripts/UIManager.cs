using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Firebase.Analytics;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject canvasGame;
    [SerializeField] private GameObject canvasPause;
    [SerializeField] private GameObject PauseButton;
    [SerializeField] private GameObject panelLose;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelGetTime;
    [SerializeField] private GameObject panelTimeUp;
    [SerializeField] private PowerPointManager powerManager;
    private GameTimer timerMy;
    private CowController player;
    private Rigidbody2D rbPlayer;
    [SerializeField] private List<Transform> playerPositions;
    private static int previousPosition;
    public GameObject[] players;
    int sceneIndex;
    public GameObject loadingScreen;
    public Slider bar;

    [SerializeField] private UserCommunicationSO userCommunicationSO;

    private void Awake()
    {
        player = Instantiate(players[PlayerPrefs.GetInt("Player")], ChooseStartPlayerPosition().position, Quaternion.identity).GetComponent<CowController>();
        rbPlayer = player.transform.GetComponentInParent<Rigidbody2D>();
    }
    public void Start()
    {
        PlayerPrefs.SetInt("timeBonus1", 0);
        PlayerPrefs.SetInt("timeBonus2", 0);
        PlayerPrefs.SetInt("timeBonus3", 0);
        timerMy = FindObjectOfType<GameTimer>();
    }

    private Transform ChooseStartPlayerPosition()
    {
        int positionIndex;
        if (playerPositions.Count > 1)
        {
            do
            {
                positionIndex = Random.Range(0, playerPositions.Count);
            }
            while (positionIndex == previousPosition);

            previousPosition = positionIndex;
        }
        else
        {
            positionIndex = 0;
        }

        return playerPositions[positionIndex];
    }
    public void PauseOn()
    {
        canvasPause.SetActive(true);
        canvasGame.SetActive(false);
        Time.timeScale = 0;
        PauseButton.GetComponent<Button>().interactable = false;
    }
    public void PauseOff()
    {
        canvasPause.SetActive(false);
        canvasGame.SetActive(true);
        Time.timeScale = 1;
        PauseButton.GetComponent<Button>().interactable = true;
    }
    public void Lose()
    {
        panelLose.SetActive(true);
        Time.timeScale = 0;
        PauseButton.GetComponent<Button>().interactable = false;
        userCommunicationSO.ChangeValueExtraLifeSpecial(1);
    }
    public void TimeUp()
    {
        panelTimeUp.SetActive(true);
        //rbPlayer.isKinematic = true;
        //rbPlayer.constraints = RigidbodyConstraints2D.FreezePosition;
        StartCoroutine(TimesUp());
        PauseButton.GetComponent<Button>().interactable = false;
    }    
    public void GetTime()
    {
        panelGetTime.SetActive(true);
        StartCoroutine(TimesUp());
        PauseButton.GetComponent<Button>().interactable = false;
    }
    public void Win()
    {
        panelWin.SetActive(true);
        Time.timeScale = 0;
        PauseButton.GetComponent<Button>().interactable = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        // powerManager.SubstractPower();
        PauseButton.GetComponent<Button>().interactable = true;
        PlayerPrefs.SetInt("AreAvailablePower", 0);
    }

    public void MiniGameRestart(string _miniGameName)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        PauseButton.GetComponent<Button>().interactable = true;
        PlayerPrefs.SetInt("AreAvailablePower", 0);

        FirebaseAnalytics.LogEvent(name: "restart_" + _miniGameName);
    }

    public void NextLevelWin()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 4)
        {
            SceneManager.LoadScene("menu-levels");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Time.timeScale = 1;
        }
    }
    public void NextLevelWinLoading()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 4)
        {
            SceneManager.LoadScene("menu-levels");
        }
        else
        {
            loadingScreen.SetActive(true);
            StartCoroutine(LoadAsync());
            //ceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
            Time.timeScale = 1;
        }
    }
    IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        while (!asyncLoad.isDone)
        {
            bar.value = asyncLoad.progress;
            Debug.Log("AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex +1);");
            yield return null;
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("menu-levels");
        Time.timeScale = 1;
    }
    public void ToTheCity()
    {
        SceneManager.LoadScene("menu-levels");
        Time.timeScale = 1;
        PlayerPrefs.SetInt("AreAvailablePower", 0);
    }
    public void PlayPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }
    IEnumerator TimesUp()
    {
        player.OnButtonUp();
        yield return new WaitForSeconds(1f);       
        Time.timeScale = 0;
    }
     public void ToTheCityShop()
    {
        SceneManager.LoadScene("menu-levels");
        Time.timeScale = 1;
        PlayerPrefs.SetInt("OpenShop", 0);
    }
}