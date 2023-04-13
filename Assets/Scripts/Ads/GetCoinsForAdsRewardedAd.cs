using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GetCoinsForAdsRewardedAd : MonoBehaviour
{
    private RewardBasedVideoAd adReward;
    private string idApp, idReward;
    [SerializeField] private Button buttonReward;
    [SerializeField] private GameObject getCoinsForAdsWindow;
    private bool _rewardedAdUsed;

    private int TotalScore;

    [SerializeField] private int dailyBonus;
    [SerializeField] AdBonusTimer bonusTimer;

    private void OnEnable()
    {
        _rewardedAdUsed = false;
        Debug.Log("OnEnable ()");
    }

    void Start()
    {
        //idApp = "ca-app-pub-4196164004824768~8007119707";
        idApp = "ca-app-pub-3940256099942544/5224354917";
        idReward = "ca-app-pub-3940256099942544/5224354917";

        adReward = RewardBasedVideoAd.Instance;
    }

    public void RequestRewardAd()
    {
        AdRequest request = AdRequestBuild();
        adReward.LoadAd(request, idReward);

        adReward.OnAdLoaded += this.HandleOnRewardedAdLoaded;
        adReward.OnAdRewarded += this.UserEarnedReward;
        adReward.OnAdClosed += this.RewardedAdClosed;
    }

    AdRequest AdRequestBuild()
    {
        return new AdRequest.Builder().Build();
    }

    public void ShowRewardAd()
    {
        if (adReward.IsLoaded())
            adReward.Show();
        Debug.Log("adReward.Show ()");
    }

    public void HandleOnRewardedAdLoaded(object sender, EventArgs args)
    {
        ShowRewardAd();
    }
    public void UserEarnedReward(object sender, EventArgs args)
    {
        TotalScore = PlayerPrefs.GetInt("TotalScore");
        TotalScore = TotalScore + dailyBonus;
        Debug.Log("!!!--- TotalScore + Bonus 250 ---!!! " + TotalScore);
        SoundManager.snd.PlaybuySounds();
        PlayerPrefs.SetInt("TotalScore", TotalScore);
        bonusTimer.AdViewed();
    }
    public void RewardedAdClosed(object sender, EventArgs args)
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
        RequestRewardAd();
    }

    private void OnDisable()
    {
        adReward.OnAdLoaded -= this.HandleOnRewardedAdLoaded;
        adReward.OnAdRewarded -= this.UserEarnedReward;
        adReward.OnAdClosed -= this.RewardedAdClosed;
    }

}