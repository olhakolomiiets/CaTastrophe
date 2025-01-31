using UnityEngine;
using System.Collections;

public class ShowUIOnce : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private GameObject uiObject;  // UI to show once
    [SerializeField] private string prefKey = "UIShown";  // Key to track if UI was shown
    [SerializeField] private bool pauseGameOnShow = false;  // Pause the game when UI is shown
    [SerializeField] private float delayToShow = 0f;  // Delay before showing UI

    [Header("Cloning Settings")]
    [SerializeField] private GameObject gameObjToCopy; // Object to be cloned
    [SerializeField] private Transform gameObjParent;   // Parent object for the clone
    [SerializeField] private GameObject alternateObject; // Alternate object to be cloned
    [SerializeField] private bool useAlternateObject = false; // Use alternate object instead of default

    [Header("Cloning Settings for Second Obj")]
    [SerializeField] private Transform gameObjToCopy2;   // Parent object for the clone 2
    [SerializeField] private Transform gameObjParent2;   // Parent object for the clone 2
    [SerializeField] private GameObject alternateObject2; // Object to be cloned

    private GameObject clonedObject;

    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstMessages") == 1)
        {
            return;
        }

        StartCoroutine(ShowUIWithDelay());
    }

    private IEnumerator ShowUIWithDelay()
    {
        yield return new WaitForSeconds(delayToShow);
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
            CloneAndParentObject(useAlternateObject && alternateObject != null ? alternateObject : gameObjToCopy);

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

    public void CloneAndParentObject(GameObject objectToClone)
    {
        if (objectToClone != null && gameObjParent != null)
        {
            clonedObject = Instantiate(objectToClone, gameObjToCopy.transform.position, gameObjToCopy.transform.rotation);
            clonedObject.transform.SetParent(gameObjParent, true);
            clonedObject.transform.localScale = Vector3.one;
            clonedObject.SetActive(true);
        }
    }

    public void CloneAndParentSecondObject(GameObject objectToClone)
    {
        if (objectToClone != null && gameObjParent != null)
        {
            clonedObject = Instantiate(objectToClone, gameObjToCopy2.transform.position, gameObjToCopy2.transform.rotation);
            clonedObject.transform.SetParent(gameObjParent2, true);
            clonedObject.transform.localScale = Vector3.one;
            clonedObject.SetActive(true);
        }
    }

    public void UseAlternateObject()
    {
        useAlternateObject = true;
    }

    private void OnDisable()
    {
        if (clonedObject != null)
        {
            Destroy(clonedObject);
        }
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
