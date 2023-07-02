using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.Events;
using Firebase.Analytics;

public class ExtraLifeRewardedAd : MonoBehaviour
{
    #region EDITOR FIELDS

    [SerializeField] private Button buttonReward;
    [SerializeField] private GameObject panelLose;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GoogleMobileAds.Sample.RewardedAdController _adController;

    #endregion

    #region UNITY EVENTS

    [HideInInspector] public UnityEvent OnUserEarnedRewardEvent;
    [HideInInspector] public UnityEvent OnAdClosedEvent;

    #endregion

    #region PRIVATE FIELDS

    private bool _rewardedAdUsed;

    #endregion


    private void OnEnable()
    {
        _adController.OnUserEarnedRewardEvent.AddListener(UserEarnedReward);
        _adController.OnAdClosedEvent.AddListener(RewardedAdClosed);

        if (!_rewardedAdUsed)
        {
            _adController.LoadAd();
            buttonReward.interactable = true;
        }
        else
        {
            buttonReward.interactable = false;
        }

        Debug.Log("OnEnable () ExtraLifeRewardedAd");
    }

    public void UserEarnedReward()
    {
        CowController.lives++;
        Debug.Log("User Earned Reward ()");

    }

    public void RewardedAdClosed()
    {
        buttonReward.GetComponentInChildren<Text>().text = $"{Lean.Localization.LeanLocalization.GetTranslationText("GetLife")}";
        Time.timeScale = 1;
        panelLose.SetActive(false);
        pauseButton.GetComponent<Button>().interactable = true;
        _rewardedAdUsed = true;
        Debug.Log("Rewarded Ad Closed ()");

        FirebaseAnalytics.LogEvent(name: "got_life_for_ads");
    }


    public void OnGetOneMoreLife()
    {
        buttonReward.interactable = false;
        buttonReward.GetComponentInChildren<Text>().text = $"{Lean.Localization.LeanLocalization.GetTranslationText("loading")}";
        _adController.ShowAd();
    }

    private void OnDisable()
    {
        _adController.OnUserEarnedRewardEvent.RemoveListener(UserEarnedReward);
        _adController.OnAdClosedEvent.RemoveListener(RewardedAdClosed);
    }


}
