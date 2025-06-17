using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdBonusTimer : MonoBehaviour
{
    #region EDITOR FIELDS

    [SerializeField] private GameObject getCoinsButton;

    #endregion

    #region PRIVATE FIELDS

    private readonly TimeSpan waitDuration = TimeSpan.FromHours(3);

    #endregion

    private void Start()
    {
        if (PlayerPrefs.HasKey("lastAdViewTime"))
        {
            if (DateTime.TryParse(PlayerPrefs.GetString("lastAdViewTime"), out DateTime lastAdViewTime))
            {
                TimeSpan timePassed = DateTime.Now - lastAdViewTime;

                if (timePassed >= waitDuration)
                {
                    ActivateButton();
                }
                else
                {
                    HideButton();
                }
            }
            else
            {
                ActivateButton();
            }
        }
        else
        {
            ActivateButton();
        }
    }

    public void AdViewed()
    {
        DateTime now = DateTime.Now;
        PlayerPrefs.SetString("lastAdViewTime", now.ToString());
        PlayerPrefs.Save();

        HideButton();
    }

    private void ActivateButton()
    {
        getCoinsButton.SetActive(true);
    }

    private void HideButton()
    {
        getCoinsButton.SetActive(false);
    }

}
