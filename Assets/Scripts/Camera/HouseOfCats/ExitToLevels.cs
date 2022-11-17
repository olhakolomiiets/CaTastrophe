using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitToLevels : MonoBehaviour
{
    public Text Total;
    int levelComplete;
    public GameObject loadingScreen;
    public Slider bar;
    private int allStars;
    public Text allStarsText;
    void Start()
    {
        levelComplete = PlayerPrefs.GetInt("LevelComplete");
    }
    private void Update()
    {
    }
    public void LoadTo(int level)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync(level));
    }
    IEnumerator LoadAsync(int level)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);
        while (!asyncLoad.isDone)
        {
            bar.value = asyncLoad.progress;
            yield return null;
        }
    }
    public void GiveMeMoney()
    {
        PlayerPrefs.SetInt("TotalScore", 10000);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("main-menu");
    }
    public void QuitApp()
    {
        Application.Quit();
    }
    
}