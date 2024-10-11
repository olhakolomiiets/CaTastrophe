using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLevels : MonoBehaviour
{
    [SerializeField]
    private Button Leve2Button;
    [SerializeField]
    private Button Leve3Button;
    [SerializeField]
    private Button Leve4Button;
    [SerializeField]
    private Button Leve5Button;
    [SerializeField]
    private Button Leve6Button;
    [SerializeField]
    private Button Leve7Button;
    [SerializeField]
    private Button Leve8Button;
    [SerializeField]
    private Button Leve9Button;
    [SerializeField]
    private Button Leve10Button;
    [SerializeField]
    private Button Leve11Button;
    [SerializeField]
    private Button Leve12Button;
    public Text Total;
    int levelComplete;
    public GameObject loadingScreen;
    public Slider bar;
    private int allStars;
    public Text allStarsText;

    private void Update()
    {
        allStars = PlayerPrefs.GetInt("AllStars");
        allStarsText.text = allStars.ToString();
    }
    public void LoadTo(int level)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync(level));

        PlayerPrefs.SetInt("LastOpenHouse", level);

        FirebaseAnalytics.LogEvent(name: "open_house", new Parameter(parameterName: "levels", parameterValue: "level " + level));        
    }

    public void MiniGameStart(int level)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync(level));

        switch (level)
        {
            case 16:
                FirebaseAnalytics.LogEvent(name: "start_Basketball");
                break;
            case 17:
                FirebaseAnalytics.LogEvent(name: "start_RoofRunner");
                break;
            case 19:
                FirebaseAnalytics.LogEvent(name: "start_BirdCatcher");
                break;
            case 20:
                FirebaseAnalytics.LogEvent(name: "start_Wall");
                break;
            case 21:
                FirebaseAnalytics.LogEvent(name: "start_NightCity1");
                break;
        }       
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

    public void Reset()
    {
        Leve2Button.interactable = false;
        Leve3Button.interactable = false;
        Total.text = "0";
        PlayerPrefs.DeleteAll();
    }

    public void GiveMeMoney()
    {
        if (PlayerPrefs.GetInt("GiveMeMoneyFirstTime") == 0)
        {
            PlayerPrefs.SetInt("TotalScore", 10000);
            PlayerPrefs.SetInt("GiveMeMoneyFirstTime", 1);

            FirebaseAnalytics.LogEvent(name: "used_GiveMeMoney");
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("main-menu");
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    private void setLevelActive(int id, Button[] listOfLevels )
    {
        for (int i = 0; i < listOfLevels.Length; i++)
        {
            if (i < id)
                listOfLevels[i].interactable = true;
            else listOfLevels[i].interactable = false;
        }
    }
}
