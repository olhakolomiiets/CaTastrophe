using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BarsPowersController : MonoBehaviour
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
    void Start()
    {
        levelComplete = PlayerPrefs.GetInt("LevelComplete");
        Button[] listOfLevels = new Button[11]{ Leve2Button, Leve3Button, Leve4Button, Leve5Button,
        Leve6Button, Leve7Button, Leve8Button, Leve9Button, Leve10Button, Leve11Button, Leve12Button };
        switch (levelComplete)
        {
            case 0:
                for (int i = 0; i < listOfLevels.Length; i++)
                {
                    listOfLevels[i].interactable = false;
                }
                break;
            case 2:
                setLevelActive(1, listOfLevels);
                break;
            case 3:
                setLevelActive(2, listOfLevels);
                break;
            case 4:
                setLevelActive(3, listOfLevels);
                break;
            case 5:
                setLevelActive(4, listOfLevels);
                break;
            case 6:
                setLevelActive(5, listOfLevels);
                break;
            case 7:
                setLevelActive(6, listOfLevels);
                break;
            case 8:
                setLevelActive(7, listOfLevels);
                break;
            case 9:
                setLevelActive(8, listOfLevels);
                break;
            case 10:
                setLevelActive(9, listOfLevels);
                break;
            case 11:
                setLevelActive(10, listOfLevels);
                break;
            case 12:
                setLevelActive(11, listOfLevels);
                break;
        }
    }
    private void Update()
    {
        allStars = PlayerPrefs.GetInt("AllStars");
        allStarsText.text = allStars.ToString();
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
    public void Reset()
    {
        Leve2Button.interactable = false;
        Leve3Button.interactable = false;
        Total.text = "0";
        PlayerPrefs.DeleteAll();
    }
    public void GiveMeMoney()
    {
        PlayerPrefs.SetInt("TotalScore", 10000);
    }
    public void GiveExstraLife()
    {
        PlayerPrefs.SetInt("extraLife", 1);
    }
    public void TakeBackExstraLife()
    {
        PlayerPrefs.SetInt("extraLife", 0);
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