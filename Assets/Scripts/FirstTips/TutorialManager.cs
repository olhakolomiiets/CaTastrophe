using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> tutorialTips = new List<GameObject>();
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private GameObject FinishButton;
    [SerializeField] private GameObject background;
    [SerializeField] private string prefForTutorial;

   private int currentIndex = 0;

    private void Start()
    {
        if (PlayerPrefs.GetInt(prefForTutorial) == 1)
        {
            return;
        }

        nextButton.interactable = true;
        prevButton.interactable = false;

        if (tutorialTips.Count > 0)
        {
            Time.timeScale = 0;
            background.SetActive(true);
            tutorialTips[currentIndex].SetActive(true);
            DisableAllExceptCurrent(currentIndex);
        }
    }

    public void ActivateNext()
    {
        tutorialTips[currentIndex].SetActive(false);
        currentIndex = (currentIndex + 1) % tutorialTips.Count;
        tutorialTips[currentIndex].SetActive(true);
        DisableAllExceptCurrent(currentIndex);
        SoundManager.snd.PlayButtonsSound();

        UpdateButtonStates();
    }

    public void ActivatePrevious()
    {
        tutorialTips[currentIndex].SetActive(false);
        currentIndex = (currentIndex - 1 + tutorialTips.Count) % tutorialTips.Count;
        tutorialTips[currentIndex].SetActive(true);
        DisableAllExceptCurrent(currentIndex);
        SoundManager.snd.PlayButtonsSound();

        UpdateButtonStates();
    }

    private void DisableAllExceptCurrent(int currentIndexToExclude)
    {
        for (int i = 0; i < tutorialTips.Count; i++)
        {
            if (i != currentIndexToExclude)
            {
                tutorialTips[i].SetActive(false);
            }
        }
    }
    private void UpdateButtonStates()
    {
        nextButton.interactable = currentIndex < tutorialTips.Count - 1;
        prevButton.interactable = currentIndex > 0;
        if (currentIndex == tutorialTips.Count - 1)
        {
            FinishButton.SetActive(true);
        }
        else
        {
            FinishButton.SetActive(false);
        }
    }

    public void Finish()
    {
        background.SetActive(false);
        Time.timeScale = 1;

        foreach (GameObject obj in tutorialTips)
        {
            obj.SetActive(false);
        }

        SoundManager.snd.PlayButtonsSound();
        nextButton.gameObject.SetActive(false); 
        prevButton.gameObject.SetActive(false);
        FinishButton.SetActive(false);
        PlayerPrefs.SetInt(prefForTutorial, 1);
    }

    public void StartTutorialAgain() 
    {
        Time.timeScale = 0;
        nextButton.interactable = true;
        prevButton.interactable = false;

        if (tutorialTips.Count > 0)
        {
            background.SetActive(true);
            tutorialTips[currentIndex].SetActive(true);
            DisableAllExceptCurrent(currentIndex);
        }
    }

}