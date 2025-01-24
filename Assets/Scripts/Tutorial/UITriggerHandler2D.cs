using UnityEngine;
using System.Collections;

public class UITriggerHandler2D : MonoBehaviour
{
    [Header("Trigger Settings")]
    [SerializeField] private string targetTag;  // Set tag via Inspector
    [SerializeField] private GameObject uiObject;
    [SerializeField] private bool activateUI = true; // If true, activates UI, otherwise deactivates
    [SerializeField] private bool useOnce = false;
    [SerializeField] private string prefKey = "UITriggerHandled";
    [SerializeField] private float delay = 0f;
    [SerializeField] private bool pauseGameOnUI = false; // If true, pauses the game when UI is shown/hidden

    private bool isTriggered = false;

    private void Start()
    {
        if (useOnce && PlayerPrefs.HasKey(prefKey))
        {
            if (!activateUI)
            {
                uiObject.SetActive(false);
            }
            isTriggered = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTriggered && other.CompareTag(targetTag))
        {
            StartCoroutine(HandleUI());
        }
    }

    private IEnumerator HandleUI()
    {
        yield return new WaitForSeconds(delay);
        if (uiObject != null)
        {
            uiObject.SetActive(activateUI);
            Debug.Log(activateUI ? "UI Activated" : "UI Deactivated");
        }

        if (pauseGameOnUI)
        {
            if (activateUI)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        if (useOnce)
        {
            PlayerPrefs.SetInt(prefKey, 1);
            PlayerPrefs.Save();
            isTriggered = true;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Game Resumed");
    }
}
