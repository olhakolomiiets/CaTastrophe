using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    int sceneIndex;
public GameObject loadingScreen;
    
public Slider bar;
    void Start()
    {      
        // loadingScreen.SetActive(true);
        // StartCoroutine(LoadAsync());       
    }
    IEnumerator LoadAsync () 
{
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex +1);
    while (!asyncLoad.isDone)
    {
        bar.value = asyncLoad.progress;
        Debug.Log("AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex +1);");
        yield return null;
    }
}
}