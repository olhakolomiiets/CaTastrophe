using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.Events;

public class GetCoinsRewardedAd : MonoBehaviour
{
    #region EDITOR FIELDS

    [SerializeField] private Button buttonReward;
    [SerializeField] private GameObject getCoinsForAdsWindow;

    [SerializeField] private int dailyBonus;
    [SerializeField] AdBonusTimer bonusTimer;

    [SerializeField] private GoogleMobileAds.Sample.RewardedAdController _adController;

    #endregion

    #region UNITY EVENTS

    [HideInInspector] public UnityEvent OnUserEarnedRewardEvent;
    [HideInInspector] public UnityEvent OnAdClosedEvent;

    #endregion

    #region PRIVATE FIELDS

    private bool _rewardedAdUsed;
    private int TotalScore;

    #endregion


    private void OnEnable()
    {
        _rewardedAdUsed = false;

        _adController.OnUserEarnedRewardEvent.AddListener(UserEarnedReward);
        _adController.OnAdClosedEvent.AddListener(RewardedAdClosed);

        _adController.LoadAd();

        Debug.Log("OnEnable () GetCoinsRewardedAd");
    }

    public void UserEarnedReward()
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + dailyBonus;
        Debug.Log("!!!--- TotalScore + Bonus 250 ---!!! " + TotalScore);
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);
        bonusTimer.AdViewed();
    }
    public void RewardedAdClosed()
    {
        buttonReward.interactable = true;
        buttonReward.GetComponentInChildren<Text>().text = $"{Lean.Localization.LeanLocalization.GetTranslationText("GetLife")}";
        Time.timeScale = 1;
        getCoinsForAdsWindow.SetActive(false);
        _rewardedAdUsed = true;
        Debug.Log("Rewarded Ad Closed ()");
    }

    public void GetCoins()
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