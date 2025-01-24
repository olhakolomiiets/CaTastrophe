using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowUIOnce : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject uiObject;  // UI to show once
    [SerializeField] private string prefKey = "UIShown";  // Key to track if UI was shown
    [SerializeField] private bool pauseGameOnShow = false;  // Pause the game when UI is shown
    [SerializeField] private float delayToShow = 0f;  // Delay before showing UI

    private void Start()
    {
        StartCoroutine(ShowUIWithDelay());
    }

    private IEnumerator ShowUIWithDelay()
    {
        yield return new WaitForSeconds(delayToShow);

        // Check if UI has been shown before
        if (!PlayerPrefs.HasKey(prefKey))
        {
            ShowUI();
        }
        else
        {
            uiObject.SetActive(false);
        }
    }

    private void ShowUI()
    {
        if (uiObject != null)
        {
            uiObject.SetActive(true);
            PlayerPrefs.SetInt(prefKey, 1);
            PlayerPrefs.Save();

            if (pauseGameOnShow)
            {
                PauseGame();
            }
        }
    }

    public void HideUI()
    {
        if (uiObject != null)
        {
            uiObject.SetActive(false);
            ResumeGame();
        }
    }

    public void ResetUI()
    {
        PlayerPrefs.DeleteKey(prefKey);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Paused");
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Game Resumed");
    }
}
