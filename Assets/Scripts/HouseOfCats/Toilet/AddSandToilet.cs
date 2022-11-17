using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSandToilet : MonoBehaviour, IClickable
{
    private int allSand;
    public TimerForToilet toiletTimer;
    [SerializeField] private GameObject noSandTag;
    [SerializeField] private GameObject noCleanToilet;
    
    public void Click()
    {
        allSand = PlayerPrefs.GetInt("TotalSand");
        SoundManager.snd.PlayButtonsSound();
        Debug.Log("PUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUSHHHHHHHHHHHHHHHHH");
        if (toiletTimer.isCleaned == true)
        {
            if (allSand > 0)
            {
                PlayerPrefs.SetInt("TotalSand", allSand - 1);
                toiletTimer.Click();
                toiletTimer.FillToilet();
            }
            else
            {
                StartCoroutine(NoSand());
            }
        }
        else StartCoroutine(noClean());

    }
    IEnumerator NoSand()
    {
        noSandTag.SetActive(true);
        yield return new WaitForSeconds(1f);
        noSandTag.SetActive(false);
    }
    IEnumerator noClean()
    {
        noCleanToilet.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        noCleanToilet.SetActive(false);
    }

}

