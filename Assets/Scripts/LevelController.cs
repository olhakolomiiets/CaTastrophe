using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance = null;
    int sceneIndex;
    int levelComplete;
    public GameObject loadingScreen;
    public Slider bar;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelComplete = PlayerPrefs.GetInt("LevelComplete");
    }
    // public void isEndGame()
    // {
    //     if (sceneIndex == 13)
    //     {
    //         Invoke("LoadMenuLevels", 1f);
    //     }
    //     else
    //     {
    //         if (levelComplete < sceneIndex)
    //             PlayerPrefs.SetInt("LevelComplete", sceneIndex);
    //         // loadingScreen.SetActive(true);
    //         // StartCoroutine(LoadAsync());
    //         Invoke("NextLevel", 1f);
    //     }
    // }
    IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex + 1);
        while (!asyncLoad.isDone)
        {
            bar.value = asyncLoad.progress;
            Debug.Log("AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex +1);");
            yield return null;
        }
    }
    void NextLevel()
    {
        // loadingScreen.SetActive(true);
        // StartCoroutine(LoadAsync());
        SceneManager.LoadScene(sceneIndex + 1);
    }
    void LoadMenuLevels()
    {
        SceneManager.LoadScene("menu-levels");
    }
}